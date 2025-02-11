﻿using CartMicroservice.Models;
using MongoDB.Driver;
using System.Security.AccessControl;

namespace CartMicroservice.Repositories
{
    public class CartRepository(IMongoDatabase db) : ICartRepository
    {
        private readonly IMongoCollection<Cart> _col = db.GetCollection<Cart>(Cart.DocumentName);
        

        public IList<CartItem> GetCartItems(string userId)
        {
           return _col.Find(c=>c.UserId == userId).FirstOrDefault()?.CartItems ?? new List<CartItem>();
        }

        public void InsertCartItem(string userId, CartItem cartItem)
        {
            var cart =  _col.Find(c=> c.UserId == userId).FirstOrDefault();
            if(cart == null)
            {
                cart = new Cart()
                {
                    UserId = userId,
                    CartItems = new List<CartItem> { cartItem }
                };
            }
            else
            {
                var ci = cart.CartItems.FirstOrDefault(ci=>ci.CatalogItemId == cartItem.CatalogItemId);
                if (ci == null)
                {
                    cart.CartItems.Add(cartItem);
                }
                else
                {
                    ci.Quantity++; 
                }
                var update = Builders<Cart>.Update.Set(collection => collection.CartItems, cart.CartItems);
                _col.UpdateOne(collection => collection.UserId == userId, update);
            }
        }

        public void UpdateCartItem(string userId, CartItem cartItem)
        {
            var cart = _col.Find(c => c.UserId == userId).FirstOrDefault();
            if (cart != null)
            {
                cart.CartItems.RemoveAll(ci=>ci.CatalogItemId==cartItem.CatalogItemId);
                cart.CartItems.Add(cartItem);
                var update = Builders<Cart>.Update
                    .Set(c => c.CartItems, cart.CartItems);
                _col.UpdateOne(c => c.UserId == userId, update);
            }
        }
        public void DeleteCartItem(string userId, string cartItemId)
        {
            var cart = _col.Find(c => c.UserId == userId).FirstOrDefault();
            if (cart != null)
            {
                cart.CartItems.RemoveAll(ci=>ci.CatalogItemId == cartItemId);
                var update = Builders<Cart>.Update
                    .Set(c=>c.CartItems, cart.CartItems);
                _col.UpdateOne(c => c.UserId == userId,update);
            }
        }
        private IList<Cart> GetCarts(string catalogItemId) =>
        _col.Find(c => c.CartItems.Any(i => i.CatalogItemId == catalogItemId)).ToList();
        public void UpdateCatalogItem(string catalogItemId, string name, decimal price)
        {
            var carts = GetCarts(catalogItemId);
            foreach(var cart in carts)
            {
                var cartItem = cart.CartItems.FirstOrDefault(i => i.CatalogItemId == catalogItemId);
                if(cartItem != null)
                {
                    cartItem.Name = name;
                    cartItem.Price = price;
                    var update = Builders<Cart>.Update
                        .Set(c => c.CartItems, cart.CartItems);
                    _col.UpdateOne(c => c.Id == cart.Id, update);
                }
            }
        }
        

        public void DeleteCatalogItem(string catalogItemId)
        {
            var carts = GetCarts(catalogItemId);
            foreach(var cart in carts)
            {
                cart.CartItems.RemoveAll(c=>c.CatalogItemId==catalogItemId);
                var update = Builders<Cart>.Update
                       .Set(c => c.CartItems, cart.CartItems);
                _col.UpdateOne(c => c.Id == cart.Id, update);
            }
        }
    }
}
