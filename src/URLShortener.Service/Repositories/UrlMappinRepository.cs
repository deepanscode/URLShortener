using Microsoft.Azure.Cosmos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using URLShortener.Service.Contracts;
using URLShortener.Service.Models;

namespace URLShortener.Service.Repositories
{
    public class UrlMappinRepository : IUrlMappinRepository
    {
        private readonly CosmosClient _cosmosClient;
        private readonly Database _database;
        private readonly Container _container;

        public UrlMappinRepository(CosmosDbOptions options)
        {
            _cosmosClient = new CosmosClient(options.ConnectionString, new CosmosClientOptions 
            {
                 SerializerOptions = new CosmosSerializationOptions
                 {
                     PropertyNamingPolicy = CosmosPropertyNamingPolicy.CamelCase
                 }
            });
            _database = _cosmosClient.GetDatabase(options.DatabaseId);
            _container = _database.GetContainer(options.ContainerId);
        }

        public async Task<UrlMapping> Get(string id)
        {
            return await _container.ReadItemAsync<UrlMapping>(id, new PartitionKey(id));
        }

        public async Task<UrlMapping> Save(string url)
        {
            var item = new UrlMapping
            {
                Id = Guid.NewGuid().ToString().Replace("-", ""),
                Url = url
            };
            
            var response = await _container.UpsertItemAsync<UrlMapping>(item);
            return response;
        }
    }
}
