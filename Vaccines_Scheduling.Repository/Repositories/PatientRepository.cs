using Microsoft.EntityFrameworkCore;
using Vaccines_Scheduling.Entity.DTO;
using Vaccines_Scheduling.Entity.Entities;
using Vaccines_Scheduling.Repository.Interface.IRepositories;

namespace Vaccines_Scheduling.Repository.Repositories
{
    public class PatientRepository : IPatientRepository
    {
        //public PatientRepository(Context context) : base(context) { }

        public Task<Patient> GetPatient(string login)
        {
            //var query = EntitySet.Where(e => e.Id == idUsuario);

            //return query.FirstOrDefaultAsync();
            throw new NotImplementedException();
        }

        public Task<List<PatientDTO>> ListPatient(string login)
        {
            throw new NotImplementedException();
        }

        public Task<List<PatientDTO>> GetAll()
        {
            throw new NotImplementedException();
        }

        public Task Insert(Patient patient)
        {
            throw new NotImplementedException();
        }

        public Task Delete(Patient patient)
        {
            throw new NotImplementedException();
        }
    }
}
