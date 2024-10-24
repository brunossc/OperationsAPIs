using Operations.SideCar.MQContracts;

namespace Operations.Report.API.Domain.Interfaces
{
    public interface IOperationService
    {
        Task ProcessOperation(OperationContract operation);
    }
}
