using log4net;
using Vaccines_Scheduling.Business.Interface.IBusiness;
using Vaccines_Scheduling.Entity.DTO;
using Vaccines_Scheduling.Entity.Entities;
using Vaccines_Scheduling.Entity.Model;
using Vaccines_Scheduling.Repository.Interface.IRepositories;

namespace Vaccines_Scheduling.Business.Businesses
{
    public class AppointmentSignUpBusiness : IAppointmentSignUpBusiness
    {
        private static readonly ILog _log = LogManager.GetLogger(typeof(AppointmentSignUpBusiness));
        private readonly IAppointmentSignUpRepository _appointmentRepository;
        private readonly IPatientSignUpRepository patientRepository; 

        public AppointmentSignUpBusiness(IAppointmentSignUpRepository appointmentRepository)
        {
            _appointmentRepository = appointmentRepository;

        }
        public async  Task<List<AppointmentDTO>> DeleteAppointment(int id, string patientId)
        {
            var appointment = await _appointmentRepository.GetAppointmentById(id);
            if (appointment != null)
            {
                await _appointmentRepository.Delete(appointment);
                _log.InfoFormat("O agendamento do dia '{0}' e horario '{1}' foi removido.", appointment.Date, appointment.Time);
            }
            else
            {
                _log.InfoFormat("appointment does not exist");
                throw new NotImplementedException();
            }
            var intId = Int32.Parse(patientId);
            return await _appointmentRepository.GetPatientAppointmentsById(intId);
        }

        public async Task<List<AppointmentDTO>> GetPatientAppointments(string id)
        {
            var intId = Int32.Parse(id);
            var appointment = await patientRepository.GetPatientById(intId);
            if (appointment == null)
            {
                _log.InfoFormat("Patient does not exist");
                throw new NotImplementedException();
            }
            return await _appointmentRepository.GetPatientAppointmentsById(intId);
        }

        public async Task<List<AppointmentDTO>> InsertAppointment(AppointmentSignUpModel newAppointment,  string id)
        {
            var intId = Int32.Parse(id);
            var Appointment = await _appointmentRepository.GetAppointmentById(intId);

            if (Appointment != null)
            {
                throw new NotImplementedException();
            }

            Appointment = BuildAppointment(newAppointment, intId);

            await _appointmentRepository.Insert(Appointment);

            return await _appointmentRepository.GetPatientAppointmentsById(intId);
        }

        public static Appointment BuildAppointment(AppointmentSignUpModel newAppointment, int id)
        {
            var Appointment = new Appointment
            {
                IdPatient = id,
                Date = newAppointment.Date,
                Time = newAppointment.Time,
                Status = "Não Realizado",
                CreationTime = DateTime.Now
            };

            return Appointment;
        }
    }   
}