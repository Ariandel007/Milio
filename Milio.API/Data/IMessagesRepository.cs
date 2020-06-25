using System.Collections.Generic;
using System.Threading.Tasks;
using Milio.API.Helpers;
using Milio.API.Models;

namespace Milio.API.Data
{
    public interface IMessagesRepository
    {
        void Add<T>(T entity) where T: class;
        void Delete<T>(T entity) where T: class;
        Task<bool> SaveAll();
        Task<Message> GetMessage(int id);
        Task<PagedList<Message>> GetMessagesForUser(MessageParams messageParams);
        Task<IEnumerable<Message>> GetMessageThread(int userId, int recipientId);

    }

}