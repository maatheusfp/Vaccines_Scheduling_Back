using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Vaccines_Scheduling.Business.Interface.IBusiness;
using Vaccines_Scheduling.Entity.DTO;
using Vaccines_Scheduling.Entity.Entities;
using Vaccines_Scheduling.Entity.Model;
using Vaccines_Scheduling.Repository.Interface.IRepositories;

namespace Vaccines_Scheduling.Business.Businesses
{
    public class PatientSignUpBusiness : IPatientSignUpBusiness
    {   
        private static readonly ILog _log = LogManager.GetLogger(typeof(PatientSignUpBusiness));
        private readonly IPatientSignUpRepository _patientRepository;
        public PatientSignUpBusiness(IPatientSignUpRepository patientRepository)
        {
            _patientRepository = patientRepository;
        }
        public async Task<List<PatientDTO>> DeletePatient(string login)
        {
            var patient = await _patientRepository.GetPatient(login);
            if (patient != null)
            {
                await _patientRepository.Delete(patient);
                _log.InfoFormat("O usuario '{0}' foi removido.", patient.Name); // modificar saida dps
            }
            else
            {
                _log.InfoFormat("Patient does not exist");
                throw new NotImplementedException();
            }

            return await _patientRepository.GetAll();
        }
        public async Task<List<PatientDTO>> FindPatient(string login)
        {
            var patient = await _patientRepository.GetPatient(login);
            if (patient == null)
            {
                throw new NotImplementedException();
            }

            return await _patientRepository.ListPatient(login); 
        }

        public async Task<List<PatientDTO>> InsertPatient(PatientSignUpModel newPatient)
        {
            var patient = await _patientRepository.GetPatient(newPatient.Login);

            if (patient != null)
            {
                throw new NotImplementedException();
            }

            patient = BuildPatient(newPatient);

            await _patientRepository.Insert(patient);

            return await _patientRepository.GetAll();      
        }

        public static Patient BuildPatient(PatientSignUpModel newPatient)
        {
            var patient = new Patient
            {
                Name = newPatient.Name,
                Login = newPatient.Login,
                Birthday = newPatient.Birthday,
                CreationTime = DateTime.Now
            };

            using var hmac = new HMACSHA512();
            patient.PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(newPatient.Password));
            patient.PasswordSalt = hmac.Key;

            return patient;
        }
    }
}
