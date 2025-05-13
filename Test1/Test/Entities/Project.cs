namespace Test.Entities;

public class Project
{
    public int Id {get; set;}
    public string Name {get; set;} = string.Empty;
    public DateTime Deadline {get; set;}
    public List<Task> Tasks { get; set; } = [];
}