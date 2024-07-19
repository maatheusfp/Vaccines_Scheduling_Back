using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vaccines_Scheduling.Entity.DTO;
using Vaccines_Scheduling.Entity.Entities;
using Vaccines_Scheduling.Repository.Interface.IRepositories;

namespace Vaccines_Scheduling.Repository.Repositories
{
    public class AppointmentSignUpRepository : BaseRepository<Appointment>, IAppointmentSignUpRepository
    {
        public AppointmentSignUpRepository(Context context) : base(context) { }

        public Task<List<AppointmentDTO>> GetAppointmentsByDate(DateOnly date)
        {
            var query = EntitySet.Where(e => e.Date == date)
                                 .Select(e => new AppointmentDTO
                                 {
                                     Date = e.Date,
                                     Time = e.Time,
                                     Status = e.Status,
                                 })
                                 .OrderBy(e => e.Date)
                                 .ThenBy(e => e.Time);

            return query.ToListAsync();
        }

        public Task<List<AppointmentDTO>> GetPatientAppointmentsById(int id)
        {
            var query = EntitySet.Where(e => e.IdPatient == id)
                                 .Select(e => new AppointmentDTO
                                 {
                                     Id = e.Id,
                                     Date = e.Date,
                                     Time = e.Time,
                                     Status = e.Status,
                                 })
                                 .OrderBy(e => e.Date)
                                 .ThenBy(e => e.Time);

            return query.ToListAsync();
        }

    }
}
