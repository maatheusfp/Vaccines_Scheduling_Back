using Microsoft.EntityFrameworkCore;
using Vaccines_Scheduling.Entity.DTO;
using Vaccines_Scheduling.Entity.Entities;
using Vaccines_Scheduling.Repository.Interface.IRepositories;

namespace Vaccines_Scheduling.Repository.Repositories
{
    public class PatientRepository : BaseRepository<Patient>, IPatientRepository
    {
        public  PatientRepository(Context context) : base(context) { }

        public Task<Patient> GetPatient(string login, bool asNoTracking = false)
        {
            var query = EntitySet.AsQueryable();

            //if (asNoTracking)
            //    query = query.AsNoTracking();

            return query.FirstOrDefaultAsync(e => e.Login.ToLower() == login.ToLower());
        }

        public Task<List<PatientDTO>> ListPatient(string login)
        {
            var query = EntitySet.Where(e => e.Login.ToLower() == login.ToLower())
                                 .Select(e => new PatientDTO
                                 {
                                     Name = e.Name,
                                     Login = e.Login
                                 })
                                 .OrderBy(e => e.Name)
                                 .Distinct();

            return query.ToListAsync();
        }

        public Task<List<PatientDTO>> GetAll()
        {
            var query = EntitySet.Select(patient => new PatientDTO
            {
                Name = patient.Name,
                Login = patient.Login
            })
                                 .OrderBy(e => e.Name)
                                 .Distinct();

            return query.ToListAsync();
        }
    }
}
