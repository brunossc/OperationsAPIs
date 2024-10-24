using MassTransit;
using Operations.Report.API.Application.Interfaces;
using Operations.SideCar.MQContracts;

namespace Operations.Report.API.Infrastructure.Consumer
{
    public class OperationConsumer : IConsumer<OperationContract>
    {
        private readonly IOperationServiceApp _service;
        private readonly ILogger<OperationConsumer> _logger;

        public OperationConsumer(IOperationServiceApp service, ILogger<OperationConsumer> logger)
        {
            _service = service;
            _logger = logger;
        }

        public async Task Consume(ConsumeContext<OperationContract> context)
        {
            try
            {
                await _service.HandleOperationAsync(context.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error consuming operation");
            }
        }
    }
}
