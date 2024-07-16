using Vaccines_Scheduling.Entity.DTO;
using Vaccines_Scheduling.Entity.Model;

namespace Vaccines_Scheduling.Business.Interface.IBusiness
{
    public interface IPatientSignUpBusiness
    {
        Task<string> DeletePatient(string id);
        Task<List<PatientDTO>> InsertPatient(PatientSignUpModel newPatient);
    }
}
