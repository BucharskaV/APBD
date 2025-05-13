using Test.Entities;

namespace Test.Contracts.Responses;

public class GetMemberResponse
{
    public int IdTeamMember { get; set; }
    public string FirstName {get; set;} = string.Empty;
    public string LastName {get; set;} = string.Empty;
    public string Email {get; set;} = string.Empty;
    public List<TaskInfo> TasksAssignedTo { get; set; }
    public List<TaskInfo> TasksCreated { get; set; }
    public class TaskInfo
    {
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public DateTime Deadline { get; set; }
        public ProjectInfo Project { get; set; } = null!;
        public TaskTypeInfo TaskType { get; set; }= null!;
    }
    public class ProjectInfo
    {
        public string Name { get; set; } = string.Empty;
    }

    public class TaskTypeInfo
    {
        public string Name { get; set; } = string.Empty;
    }
}