using MassTransit;
using NUlid;
using Operations.API.Application.DTO;
using Operations.API.Domain.Entities;
using Operations.API.Domain.Interfaces.Services;
using Operations.SideCar.Enum;
using Operations.SideCar.MQContracts;

namespace Operations.API.Domain.Services
{
    public class OperationServiceApp : IOperationServiceApp
    {
        private readonly IBus _bus;
        private readonly IOperationService _service;
        private readonly ILogger<OperationServiceApp> _logger;

        public OperationServiceApp(IBus bus, IOperationService service, ILogger<OperationServiceApp> logger)
        {
            _bus = bus;
            _service = service;
            _logger = logger;
        }

        public async Task AddOperationAsync(OperationDTO operationDto)
        {
            try
            {
                var operation = new Operation
                {
                    Id = Ulid.NewUlid().ToString(),
                    Day = DateTime.UtcNow,
                    Type = (int)operationDto.Type,
                    Value = operationDto.Value
                };

                await _service.AddOperationAsync(operation)
                    .ContinueWith(async (o) => 
                    {
                        if (o.IsCompletedSuccessfully)
                        {
                            var operationEvent = new OperationContract()
                            {
                                Id = operation.Id,
                                Day = operation.Day,
                                Type = (OperationType)Enum.Parse(typeof(OperationType), operation.Type.ToString()),
                                Value = operation.Value
                            };

                            await _bus.Publish(operationEvent);
                        }
                    });                

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while adding operation");
                throw;
            }
        }
    }
}
