using Test.Contracts.Responses;
using Test.Repositories.Abstractions;

namespace Test.Repositories;

public class TaskRepository : ITaskRepository
{
    private readonly IUnitOfWork _unitOfWork;

    public TaskRepository(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<GetMemberResponse?> GetMemberByIdAsync(int memberId, CancellationToken cancellationToken = default)
    {
        const string query = @"
                                    SELECT tm.FirstName, tm.LastName, tm.Email,
                   t.Name , t.Description, t.Deadline,
                   p.Name , tt.Name ,
                   t.IdAssignedTo, t.IdCreator
            FROM TeamMember tm
            LEFT JOIN Task t ON t.IdAssignedTo = tm.IdTeamMember OR t.IdCreator = tm.IdTeamMember
            LEFT JOIN Project p ON t.IdProject = p.IdProject
            LEFT JOIN TaskType tt ON t.IdTaskType = tt.IdTaskType
            WHERE tm.IdTeamMember = @memberId;
                            ";
        var con = await _unitOfWork.GetConnectionAsync();
        await using var command = con.CreateCommand();
        command.CommandText = query;
        command.Transaction = _unitOfWork.Transaction;
        command.Parameters.AddWithValue("@memberId", memberId);
        await using var reader = await command.ExecuteReaderAsync(cancellationToken);
        
        GetMemberResponse? response = null;
        while (await reader.ReadAsync(cancellationToken))
        {
            response = new GetMemberResponse
            {
                IdTeamMember = memberId,
                FirstName = reader.GetString(0),
                LastName = reader.GetString(1),
                Email = reader.GetString(2),
                TasksAssignedTo = [],
                TasksCreated = []
            };

            if (!reader.IsDBNull(9) && reader.GetInt32(9) == memberId)
            {
                GetMemberResponse.TaskInfo? taskInfo = new()
                {
                    Name = reader.GetString(3),
                    Description = reader.GetString(4),
                    Deadline = reader.GetDateTime(5),
                    Project = new GetMemberResponse.ProjectInfo()
                    {
                        Name = reader.GetString(6)
                    },
                    TaskType = new GetMemberResponse.TaskTypeInfo()
                    {
                        Name = reader.GetString(7)
                    }
                };
                response.TasksAssignedTo.Add(taskInfo);
            }
            /*if (!reader.IsDBNull(10) && reader.GetInt32(10) == memberId)
            {
                GetMemberResponse.TaskInfo? taskInfo = new()
                {
                    Name = reader.GetString(3),
                    Description = reader.GetString(4),
                    Deadline = reader.GetDateTime(5),
                    Project = new GetMemberResponse.ProjectInfo()
                    {
                        Name = reader.GetString(6)
                    },
                    TaskType = new GetMemberResponse.TaskTypeInfo()
                    {
                        Name = reader.GetString(7)
                    }
                };
                response.TasksCreated.Add(taskInfo);
            }*/
        }

        /*if (response != null)
        {
            response.TasksAssignedTo.OrderDescending().OrderByDescending(t => t.Deadline).ToList();
            response.TasksCreated.OrderDescending().OrderByDescending(t => t.Deadline).ToList();
        }*/
        return response;
    }

    public async Task<bool> MemberExistsAsync(int memberId, CancellationToken cancellationToken = default)
    {
        const string query = @"SELECT 
                                 IIF(EXISTS (SELECT 1 FROM TeamMember 
                                         WHERE TeamMember.IdTeamMember = @memberId), 1, 0);   
                                ";
        var connection = await _unitOfWork.GetConnectionAsync();
        await using var command = connection.CreateCommand();
        command.CommandText = query;
        command.Transaction = _unitOfWork.Transaction;
        command.Parameters.AddWithValue("@memberId", memberId);
        var result = Convert.ToInt32(await command.ExecuteScalarAsync(cancellationToken));
        return result == 1;
    }

    public async Task<bool> ProjectExistsAsync(int projectId, CancellationToken cancellationToken = default)
    {
        const string query = @"SELECT 
                                 IIF(EXISTS (SELECT 1 FROM Project 
                                         WHERE Project.IdProject = @projectId), 1, 0);   
                                ";
        var connection = await _unitOfWork.GetConnectionAsync();
        await using var command = connection.CreateCommand();
        command.CommandText = query;
        command.Transaction = _unitOfWork.Transaction;
        command.Parameters.AddWithValue("@projectId", projectId);
        var result = Convert.ToInt32(await command.ExecuteScalarAsync(cancellationToken));
        return result == 1;
    }

    public async Task DeleteProjectAsync(int projectId, CancellationToken cancellationToken = default)
    {
        const string query = @"Delete FROM Project WHERE Project.IdProject = @projectId;   
                                ";
        var connection = await _unitOfWork.GetConnectionAsync();
        await using var command = connection.CreateCommand();
        command.CommandText = query;
        command.Transaction = _unitOfWork.Transaction;
        command.Parameters.AddWithValue("@projectId", projectId);
        await command.ExecuteScalarAsync(cancellationToken);
    }

    public async Task DeleteTaskAsync(int projectId, CancellationToken cancellationToken = default)
    {
        const string query = @"Delete FROM Task WHERE Task.IdProject = @projectId;   
                                ";
        var connection = await _unitOfWork.GetConnectionAsync();
        await using var command = connection.CreateCommand();
        command.CommandText = query;
        command.Transaction = _unitOfWork.Transaction;
        command.Parameters.AddWithValue("@projectId", projectId);
        await command.ExecuteScalarAsync(cancellationToken);
    }
}