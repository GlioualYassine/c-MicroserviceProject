using Microsoft.Extensions.Options;
using MongoDB.Driver;
using Microsoft.Extensions.Options;

namespace CatalogMicroservice.MongoDB
{
    public class Mongo
    {
            public string connectionString { get; set; } = null!;

            public string database { get; set; } = null!;

            public string CatalogCollectionName { get; set; } = null!;
       
    }
}
