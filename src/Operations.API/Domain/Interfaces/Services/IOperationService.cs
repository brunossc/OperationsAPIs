using Operations.API.Domain.Entities;

namespace Operations.API.Domain.Interfaces.Services
{
    public interface IOperationService
    {
        Task AddOperationAsync(Operation operation);
    }
}
