using Microsoft.AspNetCore.Mvc;
using Vaccines_Scheduling.Business.Interface.IBusiness;
using Vaccines_Scheduling.Entity.DTO;
using Vaccines_Scheduling.Entity.Model;
using Vaccines_Scheduling.Utility.Attributes;

namespace Vaccines_Scheduling.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PatientSignUpController : ControllerBase
    {
        private readonly IPatientSignUpBusiness _patientBusiness;
        public PatientSignUpController(IPatientSignUpBusiness patientBusiness)
        {
            _patientBusiness = patientBusiness;
        }

        [HttpGet("FindPatient")]
        public async Task<List<PatientDTO>> FindPatient(string login)
        {
            return await _patientBusiness.FindPatient(login);
        }

        [HttpDelete("DeletePatient")]
        [MandatoryTransactions]
        public async Task<List<PatientDTO>> DeletePatient(string login)
        {
            return await _patientBusiness.DeletePatient(login);
        }

        [HttpPost("SignUp")]
        [MandatoryTransactions]
        public async Task<List<PatientDTO>> InsertPatient(PatientSignUpModel newPatient)
        {
            return await _patientBusiness.InsertPatient(newPatient);
        }
    }
}
