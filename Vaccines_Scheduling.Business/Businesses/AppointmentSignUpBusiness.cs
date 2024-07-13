using log4net;
using System.ComponentModel.DataAnnotations;
using Vaccines_Scheduling.Business.Interface.IBusiness;
using Vaccines_Scheduling.Entity.DTO;
using Vaccines_Scheduling.Entity.Entities;
using Vaccines_Scheduling.Entity.Model;
using Vaccines_Scheduling.Repository.Interface.IRepositories;
using Vaccines_Scheduling.Utility.Exceptions;
using Vaccines_Scheduling.Utility.Messages;

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
            if (appointment == null)
            {
                _log.InfoFormat("appointment does not exist");
                throw new BusinessException(string.Format(InfraMessages.NotFoundAppointment));              
            }
            await _appointmentRepository.Delete(appointment);
            _log.InfoFormat("O agendamento do dia '{0}' e horario '{1}' foi removido.", appointment.Date, appointment.Time);

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
                throw new BusinessException(string.Format(InfraMessages.NotFoundPatient));
            }
            var query = await _appointmentRepository.GetPatientAppointmentsById(intId);
            return query;
        }

        public async Task<List<AppointmentDTO>> InsertAppointment(AppointmentSignUpModel newAppointment,  string patientId)
        {
            var patientIntId = Int32.Parse(patientId);
            var appointments = await _appointmentRepository.GetAppointmentsByDate(newAppointment.Date);

            AppointmentsValidator(appointments, newAppointment.Time);

            var appointment = BuildAppointment(newAppointment, patientIntId);

            await _appointmentRepository.Insert(appointment);

            return await _appointmentRepository.GetPatientAppointmentsById(patientIntId);
        }

        public static void AppointmentsValidator(List<AppointmentDTO> appointments, TimeOnly time)
        {

            if (appointments == null) throw new BusinessException(string.Format(InfraMessages.NoAppointments));
            
            if (appointments.Count > 20) throw new BusinessException(string.Format(InfraMessages.FullDay));
            
            var appointmentsAtSameTime = appointments.Where(a => a.Time == time).ToList();

            if (appointmentsAtSameTime.Count > 1) throw new BusinessException(string.Format(InfraMessages.FullTime, time));
            
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