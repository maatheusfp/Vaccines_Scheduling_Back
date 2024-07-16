using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Vaccines_Scheduling.Business.Interface.IBusiness;
using Vaccines_Scheduling.Entity.DTO;
using Vaccines_Scheduling.Entity.Model;
using Vaccines_Scheduling.Utility.Attributes;

namespace Vaccines_Scheduling.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PatientSignUpController : ControllerBase
    {
        private readonly IPatientSignUpBusiness _patientBusiness;
        public PatientSignUpController(IPatientSignUpBusiness patientBusiness)
        {
            _patientBusiness = patientBusiness;
        }

        [Authorize]
        [HttpDelete("DeletePatient")]
        [MandatoryTransactions]
        public async Task<string> DeletePatient()
        {
            var userId = User.FindFirst(ClaimTypes.Sid)?.Value;
            return await _patientBusiness.DeletePatient(userId);
        }

        [HttpPost("SignUp")]
        [MandatoryTransactions]
        public async Task<List<PatientDTO>> InsertPatient(PatientSignUpModel newPatient)
        {
            return await _patientBusiness.InsertPatient(newPatient);
        }
    }
}
