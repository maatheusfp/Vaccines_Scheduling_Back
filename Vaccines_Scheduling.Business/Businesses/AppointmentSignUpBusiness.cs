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
        private readonly IAppointmentSignUpRepository _appointmentRepository;
        private readonly IPatientSignUpRepository _patientRepository;

        public AppointmentSignUpBusiness(IAppointmentSignUpRepository appointmentRepository, IPatientSignUpRepository patientRepository)
        {
            _appointmentRepository = appointmentRepository;
            _patientRepository = patientRepository;
        }
        public async  Task<List<AppointmentDTO>> DeleteAppointment(AppointmentChangeModel appointment, string patientId)
        {
            var intId = Int32.Parse(patientId);

            var patient = await _patientRepository.GetById(intId);
            var patientAppointments = await _appointmentRepository.GetPatientAppointmentsById(intId);
            var filteredAppointments = patientAppointments.Where(a => a.Date == appointment.Date && a.Time == appointment.Time).ToList();

            AppointmentValidate(patient, patientAppointments, filteredAppointments);

            var deletedAppointment = BuildAppointment(filteredAppointments[0], intId, appointment.Status);

            await _appointmentRepository.Delete(deletedAppointment);
            
            return await _appointmentRepository.GetPatientAppointmentsById(intId);
        }

        public async Task<List<AppointmentDTO>> ChangeAppointmentStatus(AppointmentChangeModel appointment, string id)
        {
            var intId = Int32.Parse(id);

            var patient = await _patientRepository.GetById(intId);
            var patientAppointments = await _appointmentRepository.GetPatientAppointmentsById(intId);
            var filteredAppointments = patientAppointments.Where(a => a.Date == appointment.Date && a.Time == appointment.Time).ToList();

            AppointmentValidate(patient, patientAppointments, filteredAppointments);

            var newAppointment = BuildAppointment(filteredAppointments[0], intId, appointment.Status);

            await _appointmentRepository.Update(newAppointment);

            return await _appointmentRepository.GetPatientAppointmentsById(intId); ;
        }

        public async Task<List<AppointmentDTO>> GetPatientAppointments(string id)
        {
            var intId = Int32.Parse(id);
            var patient = await _patientRepository.GetById(intId);
            if (patient == null)
            {
                throw new BusinessException(string.Format(InfraMessages.NotFoundPatient));
            }
            return await _appointmentRepository.GetPatientAppointmentsById(intId);

        }

        public async Task<List<AppointmentDTO>> InsertAppointment(AppointmentSignUpModel newAppointment,  string patientId)
        {
            var patientIntId = Int32.Parse(patientId);
            var appointments = await _appointmentRepository.GetAppointmentsByDate(newAppointment.Date);
            var patient = await _patientRepository.GetById(patientIntId);

            InsertAppointmentValidate(appointments, newAppointment, patient);

            var appointment = BuildAppointment(newAppointment, patientIntId);

            await _appointmentRepository.Insert(appointment);

            return await _appointmentRepository.GetPatientAppointmentsById(patientIntId);
        }

        // validate methods
        public static void AppointmentValidate(Patient patient, List<AppointmentDTO> patientAppointments, List<AppointmentDTO> filteredAppointments)
        {
            if (patient == null) throw new BusinessException(string.Format(InfraMessages.NotFoundPatient));

            if (patientAppointments == null) throw new BusinessException(string.Format(InfraMessages.NotFoundAppointment));

            if (filteredAppointments.Count == 0) throw new BusinessException(string.Format(InfraMessages.NotFoundAppointment));
        }
        public static void InsertAppointmentValidate(List<AppointmentDTO> appointments, AppointmentSignUpModel newAppointment, Patient patient)
        {   
            if (patient == null) throw new BusinessException(string.Format(InfraMessages.NotFoundPatient));

            if (patient.Name != newAppointment.PatientName) throw new BusinessException(string.Format(InfraMessages.InvalidName));

            var dateOnly = newAppointment.Birthday;
            var dateTime = dateOnly.ToDateTime(TimeOnly.MinValue);

            if (dateTime != patient.Birthday) throw new BusinessException(string.Format(InfraMessages.InvalidBdayMatch));

            if (appointments.Count >= 20) throw new BusinessException(string.Format(InfraMessages.FullDay));
            
            var appointmentsAtSameTime = appointments.Where(a => a.Time == newAppointment.Time).ToList();

            if (appointmentsAtSameTime.Count > 1) throw new BusinessException(string.Format(InfraMessages.FullTime, newAppointment.Time));
        }

        // build methods
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
        public static Appointment BuildAppointment(AppointmentDTO newAppointment, int id, string status)
        {
            var Appointment = new Appointment
            {
                Id = newAppointment.Id,
                IdPatient = id,
                Date = newAppointment.Date,
                Time = newAppointment.Time,
                Status = status,
                CreationTime = DateTime.Now
            };
            return Appointment;
        }
    }   
}