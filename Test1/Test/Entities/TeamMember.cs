namespace Test.Entities;

public class TeamMember
{
    public int Id {get; set;}
    public string FirstName {get; set;} = string.Empty;
    public string LastName {get; set;} = string.Empty;
    public string Email {get; set;} = string.Empty;
    public List<Task> TasksAssignedTo { get; set; } = [];
    public List<Task> TasksCreatead { get; set; } = [];
}