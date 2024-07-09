using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Vaccines_Scheduling.Business.Interface.IBusiness;
using Vaccines_Scheduling.Entity.DTO;

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
        public async Task<TokenPatientDTO> Login(string login, string password)
        {
            return await _authBusiness.Login(login, password);
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
