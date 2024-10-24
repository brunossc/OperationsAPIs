using Operations.Report.API.Application.Interfaces;
using Operations.Report.API.Domain.Entities;
using Operations.Report.API.Domain.Interfaces;
using Operations.SideCar.MQContracts;

namespace Operations.Report.API.Application.Services
{
    public class OperationServiceApp : IOperationServiceApp
    {
        private readonly ILogger<OperationServiceApp> _logger;
        private readonly IOperationService _service;
        private readonly IOperationRepository _repo;

        public OperationServiceApp(IOperationRepository repo, IOperationService service, ILogger<OperationServiceApp> logger)
        {
            _logger = logger;
            _service = service;
            _repo = repo;
        }

        public async Task<IEnumerable<OperationDay>> GetAllAsync()
        {
            return await Task.FromResult(_repo.GetAsync().AsEnumerable<OperationDay>());
        }

        public async Task HandleOperationAsync(OperationContract operation)
        {
            try
            {
                await _service.ProcessOperation(operation);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
            }
        }
    }
}
