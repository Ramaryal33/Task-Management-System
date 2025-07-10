using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using TaskManagementSystem.Web.Hubs;
using TaskManagementSystem.Web.Models;
using TaskManagementSystem.Web.Services;
using TaskStatus = TaskManagementSystem.Web.Models.TaskStatus;

namespace TaskManagementSystem.Web.Controllers;

public class TaskController : Controller
{
    private readonly TaskService _svc;
    private readonly IHubContext<NotificationHub> _hub;

    public TaskController(TaskService svc, IHubContext<NotificationHub> hub)
    {
        _svc = svc;
        _hub = hub;
    }

    /* ---------- List ---------- */
    public IActionResult Index()
    {
        var me = Request.Cookies["currentUser"] ?? "";

        var tasks = me == "Admin"
            ? _svc.GetTasks()
            : _svc.GetTasks()
                  .Where(t => t.CreatedBy == "Admin" &&
                              t.AssignedTo == me &&
                              t.Status == TaskStatus.Pending)
                  .ToList();

        ViewBag.CurrentUser = me;
        return View(tasks);
    }

    /* ---------- Create (Admin only) ---------- */
    public IActionResult Create()
    {
        if (Request.Cookies["currentUser"] != "Admin")
            return Unauthorized("Only Admin can create tasks.");

        ViewBag.Users = _svc.Users;
        return View();
    }

    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(TaskItem task, string? CurrentUser)
    {
        CurrentUser ??= Request.Cookies["currentUser"];
        if (CurrentUser != "Admin") return Unauthorized();

        if (!TryGetDeadline(out var deadline))
        {
            ViewBag.Users = _svc.Users;
            return View(task);
        }

        task.Deadline = deadline;
        task.CreatedBy = "Admin";
        task.Status = TaskStatus.Pending;
        _svc.AddTask(task);

        // Notify via alert + console message
        if (!string.IsNullOrWhiteSpace(task.AssignedTo))
        {
            await _hub.Clients.Group(task.AssignedTo)
                .SendAsync("ReceiveNotification", $"New task assigned to you: {task.Title}");

            await _hub.Clients.Group(task.AssignedTo)
                .SendAsync("ReceiveMessage", "You have been assigned a new task by Admin");
        }

        TempData["SuccessMessage"] = "Task created!";
        return RedirectToAction(nameof(Index));
    }

    /* ---------- Edit ---------- */
    public IActionResult Edit(int id)
    {
        var t = _svc.GetTaskById(id);
        if (t is null) return NotFound();

        var me = Request.Cookies["currentUser"] ?? "";
        if (me != "Admin" && me != t.AssignedTo)
            return Unauthorized("You can only edit tasks assigned to you.");

        ViewBag.Users = _svc.Users;
        return View("Create", t); // reuse form
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(TaskItem task, string? CurrentUser)
    {
        CurrentUser ??= Request.Cookies["currentUser"];
        if (CurrentUser != "Admin" && CurrentUser != task.AssignedTo)
            return Unauthorized();

        if (!TryGetDeadline(out var deadline))
        {
            ViewBag.Users = _svc.Users;
            return View("Create", task);
        }

        var originalTask = _svc.GetTaskById(task.Id);
        if (originalTask is null)
            return NotFound();

        bool isCompletedNow = originalTask.Status == TaskStatus.Pending &&
                              task.Status == TaskStatus.Completed;

        task.Deadline = deadline;
        _svc.UpdateTask(task);

        if (isCompletedNow)
        {
            await _hub.Clients.Group(task.AssignedTo)
                .SendAsync("ReceiveNotification", $"Task \"{task.Title}\" has been marked completed.");

            await _hub.Clients.Group(task.AssignedTo)
                .SendAsync("BadgeAdjust", -1);

            await _hub.Clients.Group(task.AssignedTo)
                .SendAsync("ReceiveMessage", $"Your task \"{task.Title}\" has been completed.");
        }

        TempData["SuccessMessage"] = "Task updated!";
        return RedirectToAction(nameof(Index));
    }

    /* ---------- Delete (Admin only) ---------- */
    public IActionResult Delete(int id)
    {
        var t = _svc.GetTaskById(id);
        return t is null ? NotFound() : View(t);
    }

    [HttpPost, ActionName("DeleteConfirmed"), ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id, string? CurrentUser)
    {
        CurrentUser ??= Request.Cookies["currentUser"];
        if (CurrentUser != "Admin") return Unauthorized();

        var t = _svc.GetTaskById(id);
        if (t is null) return NotFound();

        _svc.RemoveTask(id);

        await _hub.Clients.Group(t.AssignedTo)
            .SendAsync("ReceiveNotification", $"Task \"{t.Title}\" was deleted by Admin");

        await _hub.Clients.Group(t.AssignedTo)
            .SendAsync("ReceiveMessage", $"Your task \"{t.Title}\" was removed by Admin");

        TempData["SuccessMessage"] = "Task deleted!";
        return RedirectToAction(nameof(Index));
    }

    /* ---------- Helper ---------- */
    private bool TryGetDeadline(out DateTime dl)
    {
        var date = Request.Form["DeadlineDate"];
        var time = Request.Form["DeadlineTime"];
        return DateTime.TryParse($"{date} {time}", out dl);
    }
}
