using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Milio.API.Helpers;
using Milio.API.Models;

namespace Milio.API.Data
{
    public class UsersRepository : IUsersRepository
    {
        private readonly DataContext _context;

        public UsersRepository(DataContext context)
        {
            this._context = context;
        }

        public void Add<T>(T entity) where T : class
        {
            this._context.Add(entity);
        }

        public void Delete<T>(T entity) where T : class
        {
            this._context.Remove(entity);
        }

        public async Task<User> GetUser(int id)
        {
            var user = await _context.Users.Include(p => p.Photos).FirstOrDefaultAsync(u => u.Id == id);

            return user;
        }

        public async Task<PagedList<User>> GetCarers(UserParams userParams)
        {
            //No vamos a ejecutar este metodo a este tiempo razon por la cual no usaremos el
            //await operation async aca
            //solo consultaremos a los cuidadores
            var users = _context.Carers.Include(p => p.Photos).OrderByDescending(u => u.LastActive).AsQueryable();

            users= users.Where(u => u.Id != userParams.UserId);

            if(userParams.Gender != null)
                users= users.Where(u => u.Gender == userParams.Gender);
            
            //si la busqueda no esta en default
            if (userParams.MinAge != 18 || userParams.MaxAge != 99)
            {
                var minDoB = DateTime.Today.AddYears(-userParams.MaxAge - 1);
                var maxDoB = DateTime.Today.AddYears(-userParams.MinAge);

                users = users.Where(u => u.DateOfBirth >= minDoB && u.DateOfBirth <= maxDoB);
            }

            if (userParams.MinFareForHour != 1.0f || userParams.MaxFareForHour != 9999.0f)
                users = users.Where(u => u.FareForHour >= userParams.MinFareForHour && u.FareForHour <= userParams.MaxFareForHour);

            if(!string.IsNullOrEmpty(userParams.OrderBy))
            {
                switch (userParams.OrderBy)
                {
                    case "created":
                        users = users.OrderByDescending(u => u.Created);
                        break;
                    case "fare":
                        users = users.OrderBy(u => u.FareForHour);
                        break;
                    default:
                        users = users.OrderByDescending(u => u.LastActive);
                        break;
                }
            }

            //aca si utilizamos el await y provvereomos el soruce
            return await PagedList<User>.CreateAsync(users, userParams.PageNumber, userParams.PageSize);
        }

        public async Task<bool> SaveAll()
        {
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<Carer> GetCarer(int id)
        {
            var carer = await _context.Carers.Include(p => p.Photos).FirstOrDefaultAsync(u => u.Id == id);

            return carer;        
        }
    }
}