namespace Test.Entities;

public class TaskType
{
    public int Id {get; set;}
    public string Name { get; set; } = string.Empty;
    public List<Task> Tasks { get; set; } = [];
}