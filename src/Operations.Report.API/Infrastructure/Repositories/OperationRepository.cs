using Microsoft.Extensions.Logging;
using MongoDB.Driver;
using Operations.Report.API.Domain.Entities;
using Operations.Report.API.Domain.Interfaces;
using System;
using System.Collections;
using System.Linq;

namespace Operations.Report.API.Infrastructure.Repositories
{
    public class OperationRepository : IOperationRepository
    {

        private readonly IMongoCollection<OperationDay> _collection;

        public OperationRepository(IMongoClient mongoClient)
        {
            var mongoDatabase = mongoClient.GetDatabase("Operations");
            _collection = mongoDatabase.GetCollection<OperationDay>("OperationDay");
        }

        public async Task AddAsync(OperationDay operation)
        {
            await _collection.InsertOneAsync(operation);
        }

        public IQueryable<OperationDay> GetAsync()
        {
            return  _collection.AsQueryable();
        }

        public async Task<long> UpdateAsync(OperationDay entity)
        {
            var resultOpe = await Task.Run(() =>
            {
                var filter = Builders<OperationDay>.Filter.Eq(a => a.Id, entity.Id);
                var update = Builders<OperationDay>.Update.Set(a => a.Total, entity.Total);
                var result = _collection.UpdateOne(filter, update);
                return result.ModifiedCount;
            });

            return resultOpe;
        }
    }
}
