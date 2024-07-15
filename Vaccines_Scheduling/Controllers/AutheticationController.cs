using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Vaccines_Scheduling.Business.Interface.IBusiness;
using Vaccines_Scheduling.Entity.DTO;
using Vaccines_Scheduling.Entity.Model;
using Vaccines_Scheduling.Utility.Attributes;

namespace Vaccines_Scheduling.Webapi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthenticationController : ControllerBase
    {
        private readonly IAuthenticationBusiness _authBusiness;

        public AuthenticationController(IAuthenticationBusiness authBusiness)
        {
            _authBusiness = authBusiness;
        }
        [HttpPost("Login")]
        [MandatoryTransactions]
        public async Task<TokenPatientDTO> Login(PatientLoginModel patient)
        {
            return await _authBusiness.Login(patient);
        }
        [HttpGet("refreshToken")]
        [ProducesResponseType(typeof(TokenPatientDTO), StatusCodes.Status200OK)]
        [Authorize]
        public async Task<TokenPatientDTO> RefreshToken()
        {
            return await _authBusiness.RefreshToken();

        }
    }
}
