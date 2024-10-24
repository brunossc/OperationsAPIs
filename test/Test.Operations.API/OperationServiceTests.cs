using Moq;

namespace Test.Operations.API
{
    public class OperationServiceTests
    {
        private readonly Mock<IOperationRepository> _repositoryMock;
        private readonly Mock<IBus> _busMock;
        private readonly Mock<ILogger<OperationService>> _loggerMock;
        private readonly Mock<OperationService> _serviceMock;

        private readonly OperationService _service;
        private readonly Mock<ILogger<OperationServiceApp>> _loggerServiceAppMock;
        private readonly OperationServiceApp _serviceApp;


        public OperationServiceTests()
        {
            var operation = new Operation
            {
                Id = Ulid.NewUlid().ToString(),
                Day = DateTime.Today,
                Type = (int)OperationType.Credit,
                Value = 100
            };

            _repositoryMock = new Mock<IOperationRepository>();
            _busMock = new Mock<IBus>();
            _loggerMock = new Mock<ILogger<OperationService>>();
            _service = new OperationService(_busMock.Object, _repositoryMock.Object, _loggerMock.Object);

            _serviceMock = new Mock<OperationService>(_busMock.Object, _repositoryMock.Object, _loggerMock.Object);
            _loggerServiceAppMock = new Mock<ILogger<OperationServiceApp>>();
            _serviceApp = new OperationServiceApp(_busMock.Object, _serviceMock.Object, _loggerServiceAppMock.Object);
        }

        [Fact]
        public async Task AddOperationAsync_ShouldSave()
        {
            var operation = new Operation
            {
                Id = Ulid.NewUlid().ToString(),
                Day = DateTime.Today,
                Type = (int)OperationType.Credit,
                Value = 100
            };

            //Act            
            await _service.AddOperationAsync(operation);

            //Assert
            _repositoryMock.Verify(r => r.AddAsync(operation), Times.Once);
        }

        [Fact]
        public async Task AddOperationAsync_ShouldLogError_WhenExceptionIsThrown()
        {
            // Arrange
            var operation = new Operation
            {
                Id = Ulid.NewUlid().ToString(),
                Day = DateTime.Today,
                Type = (int)OperationType.Credit,
                Value = 100
            };

            _repositoryMock.Setup(r => r.AddAsync(It.IsAny<Operation>())).ThrowsAsync(new Exception("Database error"));

            // Act & Assert
            await Assert.ThrowsAsync<Exception>(() => _service.AddOperationAsync(operation));

            _loggerMock.Verify(l => l.Log(
                LogLevel.Error,
                It.IsAny<EventId>(),
                It.IsAny<It.IsAnyType>(),
                It.IsAny<Exception>(),
                (Func<It.IsAnyType, Exception, string>)It.IsAny<object>()), Times.Once);
        }
    }
}