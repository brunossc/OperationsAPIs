using Operations.API.Application.DTO;

namespace Operations.API.Domain.Interfaces.Services
{
    public interface IOperationServiceApp
    {
        Task AddOperationAsync(OperationDTO operation);
    }
}
