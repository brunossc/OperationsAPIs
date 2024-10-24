using Microsoft.EntityFrameworkCore;
using Operations.API.Domain.Entities;
using Operations.API.Domain.Interfaces.Repositories;
using Operations.API.Infrastructure.Persistence;

namespace Operations.API.Infrastructure.Repositories
{
    public class OperationRepository : IOperationRepository
    {
        private readonly DbSet<Operation> _dbSet;

        public OperationRepository(AppDbContext context)
        {
            _dbSet = context.Set<Operation>();
        }

        public Task AddAsync(Operation operation)
        {
            _dbSet.Add(operation);
            return Task.CompletedTask;
        }

        public Task<IEnumerable<Operation>> GetAllAsync()
        {
            return Task.FromResult(_dbSet.AsEnumerable());
        }
    }
}
