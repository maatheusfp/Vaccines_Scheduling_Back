using Vaccines_Scheduling.Entity.DTO;

namespace Vaccines_Scheduling.Business.Interface.IBusiness
{
    public interface IAuthenticationBusiness
    {
        Task<TokenPatientDTO> Login(string login, string password);
        Task<TokenPatientDTO> RefreshToken();
    }
}
