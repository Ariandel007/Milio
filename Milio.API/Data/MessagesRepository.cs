using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Milio.API.Helpers;
using Milio.API.Models;

namespace Milio.API.Data
{
    public class MessagesRepository : IMessagesRepository
    {
        private readonly DataContext _context;
        public MessagesRepository(DataContext context)
        {
            this._context = context;
        }

        public void Add<T>(T entity) where T : class
        {
            _context.Add(entity);
        }

        public void Delete<T>(T entity) where T : class
        {
            _context.Remove(entity);
        }

        public async Task<Message> GetMessage(int id)
        {
            return await _context.Messages.FirstOrDefaultAsync(m => m.Id == id);
        }

        public async Task<IEnumerable<Message>> GetMessagesForUser(MessageParams messageParams)
        {
            var messages = await _context.Messages.OrderByDescending(d => d.MessageSent).ToListAsync();
            
            messages = messages.GroupBy(p => new {p.SenderId, p.RecipientId} )
                        .Select(g => g.FirstOrDefault())
                        .ToList(); 

            // switch (messageParams.MessageContainer)
            // {
            //     case "Inbox":
            //         messages = messages.Where(u => u.RecipientId == messageParams.UserId && u.RecipientDeleted == false);
            //         break;
            //     case "Outbox":
            //         messages = messages.Where(u => u.SenderId == messageParams.UserId && u.SenderDeleted == false);
            //         break;
            //     default:
            //         messages = messages.Where(u => u.RecipientId == messageParams.UserId && u.RecipientDeleted == false && u.IsRead == false);
            //         break;
            // }
            
            // return await PagedList<Message>.CreateAsync(messages, messageParams.PageNumber, messageParams.PageSize);       
            return messages; 
        }



        public async Task<IEnumerable<Message>> GetMessageThread(int userId, int recipientId)
        {
            var messages = await _context.Messages
            .Include(u => u.Sender).ThenInclude(p => p.Photos)
            .Include(u => u.Recipient).ThenInclude(p => p.Photos)
            .Where(m => m.RecipientId == userId && m.RecipientDeleted == false
                    && m.SenderId == recipientId
                    || m.RecipientId == recipientId && m.SenderId == userId
                    && m.SenderDeleted == false)
                .OrderBy(m => m.MessageSent)
                .ToListAsync();

            return messages;
        }

        public async Task<bool> SaveAll()
        {
            return await _context.SaveChangesAsync() > 0;
        }
    }

}