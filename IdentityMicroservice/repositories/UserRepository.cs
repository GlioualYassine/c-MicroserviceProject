using IdentityMicroservice.Models;
using MongoDB.Driver;

namespace IdentityMicroservice.repositories
{
    public class UserRepository(IMongoDatabase db) : IUserRepository
    {
        private readonly IMongoCollection<User> _col =  db.GetCollection<User>(User.DocumentName);
        public User? GetUser(string email)
        {
            return _col.Find(usr=>usr.Email == email).FirstOrDefault();
        }

        public void InsertUser(User user)
        {
            _col.InsertOne(user);
        }
    }
}
