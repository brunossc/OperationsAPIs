using Operations.Report.API.Domain.Entities;

namespace Operations.Report.API.Domain.Interfaces
{
    public interface IOperationRepository
    {
        Task AddAsync(OperationDay operation);
        IQueryable<OperationDay> GetAsync();
        Task<long> UpdateAsync(OperationDay sampleEntity);
    }
}
