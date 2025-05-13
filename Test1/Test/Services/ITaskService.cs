namespace Test.Services;

public interface ITaskService
{
    public Task DeleteProject(int projectId, CancellationToken cancellationToken = default);
}