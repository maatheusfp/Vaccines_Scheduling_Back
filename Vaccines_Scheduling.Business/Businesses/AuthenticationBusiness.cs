using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Vaccines_Scheduling.Business.Interface.IBusiness;
using Vaccines_Scheduling.Entity.DTO;
using Vaccines_Scheduling.Entity.Entities;
using Vaccines_Scheduling.Repository.Interface.IRepositories;
using Vaccines_Scheduling.Utility.Configurations;
using Vaccines_Scheduling.Utility.Extensions;
using Vaccines_Scheduling.Utility.PatientContext;
using Vaccines_Scheduling.Utility.Messages;
using Vaccines_Scheduling.Entity.Model;

namespace Vaccines_Scheduling.Business.Businesses
{
    public class AuthenticationBusiness : IAuthenticationBusiness
    {
        private readonly IPatientSignUpRepository _patientRepository;
        private readonly AuthConfig _authConfig;
        private readonly IPatientContext _patientContext;
        public AuthenticationBusiness(IPatientSignUpRepository PatientRepository,
                                   IOptionsMonitor<AuthConfig> authConfig,
                                   IPatientContext patientContext)
        {
            _patientRepository = PatientRepository;
            _authConfig = authConfig.CurrentValue;
            _patientContext = patientContext;
        }
        public async Task<TokenPatientDTO> Login(PatientLoginModel patient)
        {
            var PatientValido = await Autenticar(patient.Login, patient.Password);
            var Patient = await _patientRepository.GetPatient(patient.Login);
            string token;
            string refreshToken;

            if (PatientValido && Patient != null)
            {
                token = GerarToken(Patient);
                refreshToken = GerarRefreshToken(Patient);
            }
            else
                throw new UnauthorizedAccessException();

            return new TokenPatientDTO(token, refreshToken);
        }

        public async Task<TokenPatientDTO> RefreshToken()
        {
            var login = _patientContext.Login();
            var Patient = await _patientRepository.GetPatient(login);
            string token;
            string refreshToken;

            if (Patient != null)
            {
                token = GerarToken(Patient);
                refreshToken = GerarRefreshToken(Patient);
            }
            else
                throw new UnauthorizedAccessException(InfraMessages.WrongPassword);

            return new TokenPatientDTO(token, refreshToken);
        }

        public async Task<bool> Autenticar(string login, string senha)
        {
            var user = await _patientRepository.GetPatient(login);

            if (user == null)
                return false;

            using var hmac = new HMACSHA512(user.PasswordSalt);

            return hmac.ComputeHash(Encoding.UTF8.GetBytes(senha))
                        .SequenceEqual(user.PasswordHash);
        }

        public string GerarToken(Patient patient)
        {
            var expiration = DateTime.Now.AddMinutes(_authConfig.AccessTokenExpiration);

            var claims = new List<Claim>
        {
            new(ClaimTypes.Sid, patient.Id.ToString()),
            new(ClaimTypes.Name, patient.Name),
            //new(ClaimTypes.Role, patient.Perfil.ToString()),
            new("login", patient.Login),
            new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
        };

            var privateKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_authConfig.SecretKey));
            var credentials = new SigningCredentials(privateKey, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _authConfig.Issuer,
                audience: _authConfig.Audience,
                claims: claims,
                expires: expiration,
                signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        private string GerarRefreshToken(Patient Patient)
        {
            var expiration = DateTime.Now.AddMinutes(_authConfig.RefreshTokenExpiration);

            var claims = new List<Claim>
        {
            new("login", Patient.Login)
        };

            var privateKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_authConfig.SecretKey));
            var credentials = new SigningCredentials(privateKey, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _authConfig.Issuer,
                audience: _authConfig.Audience,
                claims: claims,
                expires: expiration,
                signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}

