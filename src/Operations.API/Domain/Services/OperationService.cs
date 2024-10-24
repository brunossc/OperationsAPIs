using MassTransit;
using Operations.API.Domain.Entities;
using Operations.API.Domain.Interfaces.Repositories;
using Operations.API.Domain.Interfaces.Services;

namespace Operations.API.Domain.Services
{
    public class OperationService : IOperationService
    {
        private readonly IBus _bus;
        private readonly IOperationRepository _repository;
        private readonly ILogger<OperationService> _logger;

        public OperationService(IBus bus, IOperationRepository repository, ILogger<OperationService> logger)
        {
            _bus = bus;
            _repository = repository;
            _logger = logger;
        }

        public async Task AddOperationAsync(Operation operation)
        {
            try
            {
                await _repository.AddAsync(operation);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while adding operation");
                throw;
            }
        }
    }
}
