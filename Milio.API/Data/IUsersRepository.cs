using System.Threading.Tasks;
using Milio.API.Helpers;
using Milio.API.Models;

namespace Milio.API.Data
{
    public interface IUsersRepository
    {
        void Add<T>(T entity) where T: class;
        void Delete<T>(T entity) where T: class;
        Task<bool> SaveAll();
        Task<PagedList<User>> GetUsers(UserParams userParams);
        Task<User> GetUser(int id);
    }

}