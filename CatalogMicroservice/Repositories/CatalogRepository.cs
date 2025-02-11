﻿using CatalogMicroservice.Models;
using MongoDB.Driver;

namespace CatalogMicroservice.Repositories
{
    public class CatalogRepository(IMongoDatabase db) : ICatalogRepository
    {
        private readonly IMongoCollection<CatalogItem> _col = db.GetCollection<CatalogItem>(CatalogItem.DocumentName);
        public IList<CatalogItem> GetCatalogItems()
        {
            return _col.Find(FilterDefinition<CatalogItem>.Empty).ToList();
        }
        public CatalogItem? GetCatalogItem(string catalogItemId)
        {
            return _col.Find(catalogItem=>catalogItem.Id == catalogItemId).FirstOrDefault();
        }

        public void InsertCatalogItem(CatalogItem catalogItem)
        {
            _col.InsertOne(catalogItem);
        }

        public void UpdateCatalogItem(CatalogItem catalogItem)
        {
            _col.UpdateOne(c => c.Id == catalogItem.Id,
                Builders<CatalogItem>.Update
                .Set(c => c.Name, catalogItem.Name)
                .Set(c => c.Description, catalogItem.Description)
                .Set(c => c.Price, catalogItem.Price)
                );
        }
        public void DeleteCatalogItem(string catalogItemId)
        {
            _col.DeleteOne(c => c.Id == catalogItemId);
        }

    }
}
