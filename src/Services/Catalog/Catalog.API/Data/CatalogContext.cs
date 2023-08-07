using Catalog.API.Data.Interfaces;
using Catalog.API.Entities;
using MongoDB.Driver;
using MongoDB.Driver.Core.Configuration;

namespace Catalog.API.Data
{
    public class CatalogContext : ICatalogContext
    {
        public CatalogContext(IConfiguration configuration) {
            var Client = new MongoClient(configuration.GetValue<string>("DatabaseSettings:ConnectionString"));
            var Database = Client.GetDatabase(configuration.GetValue<string>("DatabaseSettings:DatabaseName"));
            Products = Database.GetCollection<Product>(configuration.GetValue<string>("DatabaseSettings:CollectionName"));
            CatalogContextSeed.SeedData(Products);
        }
        public IMongoCollection<Product> Products { get; }
    }
}
