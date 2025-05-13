using Test.Contracts.Responses;

namespace Test.Repositories.Abstractions;

public interface ITaskRepository 
{
    public Task<GetMemberResponse?> GetMemberByIdAsync(int memberId, CancellationToken cancellationToken = default);
    public Task<bool> MemberExistsAsync(int memberId, CancellationToken cancellationToken = default);
    public Task<bool> ProjectExistsAsync(int projectId, CancellationToken cancellationToken = default);
    public Task DeleteProjectAsync(int projectId, CancellationToken cancellationToken = default);
    
    public Task DeleteTaskAsync(int projectId, CancellationToken cancellationToken = default);
}