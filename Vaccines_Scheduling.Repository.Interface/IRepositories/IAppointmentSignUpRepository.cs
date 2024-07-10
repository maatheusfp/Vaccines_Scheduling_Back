using Vaccines_Scheduling.Entity.DTO;
using Vaccines_Scheduling.Entity.Entities;

namespace Vaccines_Scheduling.Repository.Interface.IRepositories
{
    public interface IAppointmentSignUpRepository : IBaseRepository<Appointment>
    {
        Task<Appointment> GetAppointmentById(int id);
        Task<List<AppointmentDTO>> GetPatientAppointmentsById(int id);

    }
}
