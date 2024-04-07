using CatalogMicroservice.Models;
using CatalogMicroservice.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CatalogMicroservice.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CatalogController(ICatalogRepository catalogRepository):ControllerBase
    {
        // get : api/CatalogController
        [HttpGet]
        [Authorize]
        public IActionResult Get()
        {
            var catalogItems = catalogRepository.GetCatalogItems();
            return Ok(catalogItems);
        }
        [Authorize]
        //get : api/CatalogController/{id}
        [HttpGet("{id}")]
        public IActionResult Get(string id) { 
            var  catalogItem = catalogRepository.GetCatalogItem(id);
            return Ok(catalogItem);
        }
        //post : api/CatalogController
        [HttpPost]
        public IActionResult Post([FromBody] CatalogItem catalogItem) {
           catalogRepository.InsertCatalogItem(catalogItem);
            return CreatedAtAction(nameof(Get),new {id=catalogItem.Id},catalogItem);
        }
        //put :  api/CatalogController
        [HttpPut]
        public IActionResult Put([FromBody] CatalogItem? catalogItem) {
            if (catalogItem != null)
            {
                catalogRepository.UpdateCatalogItem(catalogItem);
                return Ok();
            }
            return new NoContentResult();
        }

        //delte : api/CatalogController/{id}
        [HttpDelete("{id}")]
        public IActionResult Delete(string id)
        {
            catalogRepository.DeleteCatalogItem(id);
            return Ok();
        }
    }
}
