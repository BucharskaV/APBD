using Test.Contracts.Responses;

namespace Test.Services;

public interface ITaskService
{
    public Task DeleteProjectAsync(int projectId, CancellationToken cancellationToken = default);
    public Task<GetMemberResponse?> GetMemberAsync(int memberId, CancellationToken cancellationToken = default);
}