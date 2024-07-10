using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vaccines_Scheduling.Entity.DTO;
using Vaccines_Scheduling.Entity.Model;

namespace Vaccines_Scheduling.Business.Interface.IBusiness
{
    public interface IAppointmentSignUpBusiness
    { 
        Task<List<AppointmentDTO>> GetPatientAppointments(string id);
        Task<List<AppointmentDTO>> DeleteAppointment(int id, string patientId);
        Task<List<AppointmentDTO>> InsertAppointment(AppointmentSignUpModel newAppointment, string id);
    }
}
