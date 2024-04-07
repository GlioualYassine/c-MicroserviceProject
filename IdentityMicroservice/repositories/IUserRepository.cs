using IdentityMicroservice.Models;

namespace IdentityMicroservice.repositories
{
    public interface IUserRepository
    {
        User? GetUser(string email);
        void InsertUser(User user);
    }
}
