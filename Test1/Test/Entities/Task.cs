namespace Test.Entities;

public class Task
{
    public int Id {get; set;}
    public string Name {get; set;} = string.Empty;
    public string Description {get; set;} = string.Empty;
    public DateTime Deadline {get; set;}
    public Project Project { get; set; } = null!;
    public TaskType TaskType { get; set; } = null!;
    public TeamMember AssignedTo { get; set; } = null!;
    public TeamMember Creator { get; set; } = null!;
}