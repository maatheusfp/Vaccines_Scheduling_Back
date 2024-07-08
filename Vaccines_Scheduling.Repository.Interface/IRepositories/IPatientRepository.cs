using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vaccines_Scheduling.Entity.DTO;
using Vaccines_Scheduling.Entity.Entities;

namespace Vaccines_Scheduling.Repository.Interface.IRepositories
{
    public interface IPatientRepository : IBaseRepository<Patient>
    {
        Task<Patient> GetPatient(string login, bool asNoTracking = false);
        Task<List<PatientDTO>> ListPatient(string login);
        Task<List<PatientDTO>> GetAll();
    }
}
