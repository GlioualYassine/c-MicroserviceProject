using CartMicroservice.Models;
using CartMicroservice.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CartMicroservice.Controllers
{
    [Route("api/[controller]")]
    [ApiController]    
    public class CartController(ICartRepository cartRepository):ControllerBase
    {
        //GET : api/cart
        [HttpGet]
        [Authorize]
        public IActionResult Get([FromQuery(Name = "u")] string userId)
        {
            var cartItems = cartRepository.GetCartItems(userId);
            return Ok(cartItems); 
        }

        //POST api/Cart
        [HttpPost]
        [Authorize]
        public IActionResult Post([FromQuery(Name ="u")] string userId, [FromBody]CartItem cartItem)
        {
            cartRepository.InsertCartItem(userId, cartItem);
            return Ok();
        }

        //PUT : api/Cart/update-catalog-item
        [HttpPut("update-catalog-item")]
        [Authorize]
        public IActionResult Put([FromQuery(Name = "ci")] string catalogItemId,
            [FromQuery(Name = "n")] string name , [FromQuery(Name ="p")]decimal price)
        {
            cartRepository.UpdateCatalogItem(catalogItemId, name, price);
            return Ok();
        }

        //DELETE : api/Cart/delete-catalog-item
        [HttpDelete("delete-catalog-item")]
        [Authorize]
        public IActionResult Delete([FromQuery(Name ="ci")]string catalogItemId)
        {
            cartRepository.DeleteCatalogItem(catalogItemId);
            return Ok();
        }     
    }
}
