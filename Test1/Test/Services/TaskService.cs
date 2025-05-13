using Test.Exceptions;
using Test.Repositories;
using Test.Repositories.Abstractions;

namespace Test.Services;

public class TaskService : ITaskService
{
    private readonly ITaskRepository _taskRepository;
    private readonly IUnitOfWork _unitOfWork;

    public TaskService(ITaskRepository taskRepository, IUnitOfWork unitOfWork)
    {
        _taskRepository = taskRepository;
        _unitOfWork = unitOfWork;
    }


    public async Task DeleteProject(int projectId, CancellationToken cancellationToken = default)
    {
        await _unitOfWork.BeginTransactionAsync();
        try
        {
            if(projectId <= 0)
                throw new EnteredDataIsWrongException();
            if (!await _taskRepository.ProjectExistsAsync(projectId, cancellationToken))
                throw new NoProjectWithProvidedIdException(projectId);
            
            await _taskRepository.DeleteTaskAsync(projectId, cancellationToken);
            await _taskRepository.DeleteProjectAsync(projectId, cancellationToken);
            await _unitOfWork.CommitTransactionAsync();
        }
        catch (Exception)
        {
            await _unitOfWork.RollbackTransactionAsync();
            throw;
        }
    }
}