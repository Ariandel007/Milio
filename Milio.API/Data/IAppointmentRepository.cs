using System.Collections.Generic;
using System.Threading.Tasks;
using Milio.API.Models;

namespace Milio.API.Data
{
    public interface IAppointmentRepository
    {
        void Add<T>(T entity) where T: class;
        void Delete<T>(T entity) where T: class;
        Task<bool> SaveAll();
        // Task<Appointment> CreateAppointment(int id);
        Task<IEnumerable<Appointment>> GetAppointments(int carerId);
        Task<Appointment> GetAppointment(int id);

        // Task<IEnumerable<Message>> ConfirmAppointment(int userId);


    }

}