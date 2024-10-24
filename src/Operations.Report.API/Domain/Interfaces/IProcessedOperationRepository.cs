using Operations.Report.API.Domain.Entities;

namespace Operations.Report.API.Domain.Interfaces
{
    public interface IProcessedOperationRepository
    {
        Task AddProcessedAsync(ProcessedOperation processedOperation);
    }
}
