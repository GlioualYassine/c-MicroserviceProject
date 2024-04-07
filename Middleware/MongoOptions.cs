using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Middleware
{
    public  class MongoOptions
    {
        public string connectionString { get; set; } = null!;

        public string database { get; set; } = null!;

        //public string CatalogCollectionName { get; set; } = null!;
    }
}
