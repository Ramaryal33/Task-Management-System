using System.Text.Json;
using TaskManagementSystem.Web.Models;

namespace TaskManagementSystem.Web.Services;

public class TaskService
{
    private readonly string _file = Path.Combine(Directory.GetCurrentDirectory(), "Data", "tasks.json");
    private readonly List<TaskItem> _tasks;
    internal readonly IEnumerable<object> AllTasks;

    public List<User> Users { get; } = new() {
        new() { Name = "Admin" }, new() { Name = "User1" }, new() { Name = "User2" }
    };

    public TaskService()
    {
        Directory.CreateDirectory(Path.GetDirectoryName(_file)!);
        _tasks = File.Exists(_file)
            ? JsonSerializer.Deserialize<List<TaskItem>>(File.ReadAllText(_file)) ?? new()
            : new();
    }
    private void Save() => File.WriteAllText(_file, JsonSerializer.Serialize(_tasks));

    public List<TaskItem> GetTasks() => _tasks;

    public TaskItem? GetTaskById(int id) => _tasks.FirstOrDefault(t => t.Id == id);

    public void AddTask(TaskItem t)
    {
        t.Id = _tasks.Count == 0 ? 1 : _tasks.Max(x => x.Id) + 1;
        _tasks.Add(t); Save();
    }
    public void UpdateTask(TaskItem up)
    {
        var e = GetTaskById(up.Id); if (e is null) return;
        e.Title = up.Title; e.Description = up.Description; e.Deadline = up.Deadline;
        e.AssignedTo = up.AssignedTo; e.Status = up.Status; Save();
    }
    public void RemoveTask(int id) { _tasks.RemoveAll(t => t.Id == id); Save(); }
}
