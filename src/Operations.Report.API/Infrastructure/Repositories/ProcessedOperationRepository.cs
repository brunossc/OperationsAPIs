using MongoDB.Driver;
using Operations.Report.API.Domain.Entities;
using Operations.Report.API.Domain.Interfaces;

namespace Operations.Report.API.Infrastructure.Repositories
{
    public class ProcessedOperationRepository : IProcessedOperationRepository
    {
        private readonly IMongoCollection<ProcessedOperation> _collection;

        public ProcessedOperationRepository(IMongoClient mongoClient)
        {
            var mongoDatabase = mongoClient.GetDatabase("Operations");
            _collection = mongoDatabase.GetCollection<ProcessedOperation>("ProcessedOperations");
        }

        public async Task AddProcessedAsync(ProcessedOperation processedOperation)
        {
            await _collection.InsertOneAsync(processedOperation);
        }
    }
}
