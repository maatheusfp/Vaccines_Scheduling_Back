using Vaccines_Scheduling.Entity.DTO;
using Vaccines_Scheduling.Entity.Model;

namespace Vaccines_Scheduling.Business.Interface.IBusiness
{
    public interface IPatientBusiness
    {
        Task<List<PatientDTO>> FindPatient(string login);
        Task<List<PatientDTO>> DeletePatient(string login);
        Task<List<PatientDTO>> InsertPatient(PatientSignUpModel newPatient);
    }
}
