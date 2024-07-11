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
        private readonly IPatientSignUpRepository _patientRepository; 

        public AppointmentSignUpBusiness(IAppointmentSignUpRepository appointmentRepository, IPatientSignUpRepository patientRepository)
        {
            _appointmentRepository = appointmentRepository;
            _patientRepository = patientRepository;

        }
        public async  Task<List<AppointmentDTO>> DeleteAppointment(int appointmentId, string patientId)
        {
            var appointment = await _appointmentRepository.GetAppointmentById(appointmentId);
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
            var patient = await _patientRepository.GetPatientById(intId);
            if (patient == null)
            {
                _log.InfoFormat("Patient does not exist");
                throw new NotImplementedException();
            }
            var query = await _appointmentRepository.GetPatientAppointmentsById(intId);
            return query;
        }

        public async Task<List<AppointmentDTO>> InsertAppointment(AppointmentSignUpModel newAppointment,  string patientId)
        {
            var patientIntId = Int32.Parse(patientId);
            //var Appointment = await _appointmentRepository.GetAppointmentById(patientIntId);

            //if (Appointment != null)
            //{
            //    throw new NotImplementedException();
            //}

            var appointment = BuildAppointment(newAppointment, patientIntId);

            await _appointmentRepository.Insert(appointment);

            return await _appointmentRepository.GetPatientAppointmentsById(patientIntId);
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