using Operations.Report.API.Domain.Entities;
using Operations.SideCar.MQContracts;

namespace Operations.Report.API.Application.Interfaces
{
    public interface IOperationServiceApp
    {
        Task HandleOperationAsync(OperationContract operation);
        Task<IEnumerable<OperationDay>> GetAllAsync();
    }
}
