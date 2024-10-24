using Operations.API.Domain.Entities;

namespace Operations.API.Domain.Interfaces.Repositories
{
    public interface IOperationRepository
    {
        Task AddAsync(Operation operation);
        Task<IEnumerable<Operation>> GetAllAsync();
    }
}
