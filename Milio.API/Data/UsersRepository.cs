using System.Threading.Tasks;
using Milio.API.Models;

namespace Milio.API.Data
{
    public class UsersRepository : IUsersRepository
    {
        public void Add<T>(T entity) where T : class
        {
            throw new System.NotImplementedException();
        }

        public void Delete<T>(T entity) where T : class
        {
            throw new System.NotImplementedException();
        }

        public Task<User> GetUser(int id)
        {
            throw new System.NotImplementedException();
        }

        // public Task<PagedList<User>> GetUsers(UserParams userParams)
        // {
        //     throw new System.NotImplementedException();
        // }

        public Task<bool> SaveAll()
        {
            throw new System.NotImplementedException();
        }
    }
}