using CatalogMicroservice.Models;

namespace CatalogMicroservice.Repositories
{
    public interface ICatalogRepository
    {
        IList<CatalogItem> GetCatalogItems();
        CatalogItem? GetCatalogItem(string  catalogItemId);
        void InsertCatalogItem(CatalogItem catalogItem);
        void UpdateCatalogItem(CatalogItem catalogItem);
        void DeleteCatalogItem(string catalogItemId);
    }
}
