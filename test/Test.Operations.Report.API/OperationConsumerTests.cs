using MassTransit;
using Microsoft.Extensions.Logging;
using Moq;
using Operations.Report.API.Application.Interfaces;
using Operations.Report.API.Application.Services;
using Operations.Report.API.Domain.Entities;
using Operations.Report.API.Domain.Interfaces;
using Operations.Report.API.Domain.Services;
using Operations.Report.API.Infrastructure.Consumer;
using Operations.SideCar.Enum;
using Operations.SideCar.MQContracts;

namespace Test.Operations.Report.API
{
    public class OperationConsumerTests
    {
        private readonly Mock<IOperationService> _operationServiceMock;
        private readonly Mock<IOperationServiceApp> _operationServiceAppMock;
        private readonly Mock<ILogger<OperationConsumer>> _loggerMock;
        private readonly OperationConsumer _consumer;

        private readonly Mock<ILogger<OperationService>> _loggerOSMock;
        private readonly Mock<IOperationRepository> _operationRepositoryMock;
        private readonly Mock<IProcessedOperationRepository> _processedOperationRepositoryMock;
        private readonly OperationService _operationService;

        public OperationConsumerTests()
        {
            _operationServiceAppMock = new Mock<IOperationServiceApp>();
            _operationServiceMock = new Mock<IOperationService>();
            _loggerMock = new Mock<ILogger<OperationConsumer>>();
            _consumer = new OperationConsumer(_operationServiceAppMock.Object, _loggerMock.Object);

            _operationRepositoryMock = new Mock<IOperationRepository>();
            _processedOperationRepositoryMock = new Mock<IProcessedOperationRepository>();
            _loggerOSMock = new Mock<ILogger<OperationService>>();

            _operationService = new OperationService(_operationRepositoryMock.Object, _processedOperationRepositoryMock.Object, _loggerOSMock.Object);
        }

        [Fact]
        public async Task Consume_ShouldProcessCreditOperation()
        {
            var operation = new OperationContract { Id = "1", Day = DateTime.Today, Type = OperationType.Credit, Value = 100 };
            var contextMock = new Mock<ConsumeContext<OperationContract>>();
            contextMock.Setup(x => x.Message).Returns(operation);

            //Act
            await _consumer.Consume(contextMock.Object);

            //Assert
            _operationServiceAppMock.Verify(r => r.HandleOperationAsync(It.Is<OperationContract>(o => o.Value == 100)), Times.Once);
        }

        [Fact]
        public async Task Consume_ShouldOperationService()
        {
            var operation = new OperationContract { Id = "1", Day = DateTime.Today, Type = OperationType.Credit, Value = 100 };
            var operationDay = new OperationDay { Id = "1", Day = DateTime.Today, Total = 100 };

            //Act
            await _operationService.ProcessOperation(operation);

            //Assert
            _operationRepositoryMock.Verify(r => r.AddAsync(It.Is<OperationDay>(o => o.Total == 100)), Times.Once);
        }
    }
}