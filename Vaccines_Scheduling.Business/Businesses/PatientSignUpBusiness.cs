using log4net;
using System.Security.Cryptography;
using System.Text;
using Vaccines_Scheduling.Business.Interface.IBusiness;
using Vaccines_Scheduling.Entity.DTO;
using Vaccines_Scheduling.Entity.Entities;
using Vaccines_Scheduling.Entity.Model;
using Vaccines_Scheduling.Repository.Interface.IRepositories;
using Vaccines_Scheduling.Utility.Exceptions;
using Vaccines_Scheduling.Utility.Messages;

namespace Vaccines_Scheduling.Business.Businesses
{
    public class PatientSignUpBusiness : IPatientSignUpBusiness
    {   
        private readonly IPatientSignUpRepository _patientRepository;
        private readonly IAppointmentSignUpRepository _appointmentRepository;
        public PatientSignUpBusiness(IPatientSignUpRepository patientRepository, IAppointmentSignUpRepository appointmentRepository)
        {
            _patientRepository = patientRepository;
            _appointmentRepository = appointmentRepository;
        }
        public async Task<string> DeletePatient(string id)
        {
            var intId = Int32.Parse(id);
            var patient = await _patientRepository.GetById(intId);
            if (patient == null)
            {
                throw new BusinessException(string.Format(InfraMessages.NotFoundPatient));
            }

            var appointments = _appointmentRepository.GetPatientAppointmentsById(intId);

            if (appointments.Result.Count > 0)
            {
                throw new BusinessException(string.Format(InfraMessages.PatientHasAppointments));
            }


            await _patientRepository.Delete(patient);

            return string.Format(InfraMessages.DeleteSuccessfull);
        }

        public async Task<List<PatientDTO>> InsertPatient(PatientSignUpModel newPatient)
        {
            var patient = await _patientRepository.GetPatient(newPatient.Login);

            if (patient != null)
            {
                throw new BusinessException(string.Format(InfraMessages.RegisteredPatient));
            }

            patient = BuildPatient(newPatient);

            await _patientRepository.Insert(patient);

            return await _patientRepository.ListPatient(patient.Login);      
        }

        public static Patient BuildPatient(PatientSignUpModel newPatient)
        {
            var dateOnly = newPatient.Birthday;
            var dateTime = dateOnly.ToDateTime(TimeOnly.MinValue);

            var patient = new Patient
            {
                Name = newPatient.Name,
                Login = newPatient.Login,
                Birthday = dateTime,
                CreationTime = DateTime.Now
            };

            using var hmac = new HMACSHA512();
            patient.PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(newPatient.Password));
            patient.PasswordSalt = hmac.Key;

            return patient;
        }
    }
}
