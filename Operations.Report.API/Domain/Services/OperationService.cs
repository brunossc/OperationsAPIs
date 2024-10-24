using Operations.Report.API.Domain.Entities;
using Operations.Report.API.Domain.Interfaces;
using Operations.SideCar.Enum;
using Operations.SideCar.MQContracts;
using System.Diagnostics;

namespace Operations.Report.API.Domain.Services
{
    public class OperationService : IOperationService
    {
        private readonly IOperationRepository _repository;
        private readonly IProcessedOperationRepository _processedRepository;
        private readonly ILogger<OperationService> _logger;

        public OperationService(IOperationRepository repo, IProcessedOperationRepository processedRepository, ILogger<OperationService> logger)
        {
            _processedRepository = processedRepository;
            _repository = repo;
            _logger = logger;
        }

        public async Task ProcessOperation(OperationContract operation)
        {
            try
            {
                var operationDay = _repository.GetAsync().Where(o => o.Day.Date == operation.Day.Date).FirstOrDefault();

                if (operationDay is null)
                {
                    operationDay = new OperationDay()
                    {
                        Id = operation.Id,
                        Day = operation.Day,
                        Total = operation.Value
                    };

                    await _repository.AddAsync(operationDay);
                }
                else
                {
                    lock (operationDay)
                    {
                        operationDay.Total += operation.Type == OperationType.Credit ? operation.Value : -operation.Value;
                    }

                    await _repository.UpdateAsync(operationDay);
                }

                var processedOperation = new ProcessedOperation
                {
                    Id = operation.Id,
                    Day = operation.Day,
                    Type = operation.Type,
                    Value = operation.Value
                };

                await _processedRepository.AddProcessedAsync(processedOperation);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
            }
        }
    }
}
