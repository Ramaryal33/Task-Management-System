using Microsoft.AspNetCore.Mvc;
using TaskManagementSystem.Web.Models;
using TaskManagementSystem.Web.Services;
using TaskStatus = TaskManagementSystem.Web.Models.TaskStatus;

namespace TaskManagementSystem.Web.Controllers;

public class NotificationController : Controller
{
    private readonly TaskService _svc;

    public NotificationController(TaskService svc) => _svc = svc;

    public IActionResult Index()
    {
        var me = Request.Cookies["currentUser"] ?? "";
        if (string.IsNullOrEmpty(me))
            return Unauthorized("Please log in first.");

        var allTasks = _svc.GetTasks();

        if (me == "Admin")
        {
            // ✅ Admin: Only tasks that are assigned (to anyone)
            var assignedTasks = allTasks
                .Where(t => !string.IsNullOrWhiteSpace(t.AssignedTo))
                .ToList();
            return View(assignedTasks);
        }
        else
        {
            // ✅ Regular user: Only their pending tasks
            var myPending = allTasks
                .Where(t => t.AssignedTo == me && t.Status == TaskStatus.Pending)
                .ToList();
            return View(myPending);
        }
    }
}
