using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Milio.API.Helpers;
using Milio.API.Models;

namespace Milio.API.Data
{
    public class AppointmentRepository : IAppointmentRepository
    {
        private readonly DataContext _context;

        public AppointmentRepository(DataContext context)
        {
            this._context = context;
        }

        public void Add<T>(T entity) where T : class
        {
            _context.Add(entity);
        }

        // public async Task<Appointment> CreateAppointment(int id)
        // {
        //     this._context.Messages.Add()
        // }

        public void Delete<T>(T entity) where T : class
        {
            _context.Remove(entity);
        }

        public async Task<Appointment> GetAppointment(int id)
        {
            return await this._context.Appointments.FirstOrDefaultAsync(a => a.Id == id);
        }

        public async Task<IEnumerable<Appointment>> GetAppointments(int carerId)
        {
            return await this._context.Appointments.Where(a => a.CarerId == carerId).ToListAsync();
        }

        public async Task<bool> SaveAll()
        {
            return await _context.SaveChangesAsync() > 0;
        }
    }
}