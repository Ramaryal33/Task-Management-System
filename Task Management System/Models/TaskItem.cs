namespace TaskManagementSystem.Web.Models;

public enum TaskStatus { Pending, Completed }

public class TaskItem
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public DateTime Deadline { get; set; }
    public string CreatedBy { get; set; } = string.Empty;
    public string AssignedTo { get; set; } = string.Empty;
    public TaskStatus Status { get; set; } = TaskStatus.Pending;
}
