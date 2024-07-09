using Vaccines_Scheduling.Entity.DTO;
using Vaccines_Scheduling.Entity.Model;

namespace Vaccines_Scheduling.Business.Interface.IBusiness
{
    public interface IAuthenticationBusiness
    {
        Task<TokenPatientDTO> Login(PatientLoginModel patient);
        Task<TokenPatientDTO> RefreshToken();
    }
}
