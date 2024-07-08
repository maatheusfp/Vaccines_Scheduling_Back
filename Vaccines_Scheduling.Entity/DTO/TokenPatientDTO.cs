namespace Vaccines_Scheduling.Entity.DTO
{
    public class TokenPatientDTO
    {
        public string Token { get; set; }
        public string RefreshToken { get; set; }
        public TokenPatientDTO(string token, string refreshToken)
        {
            Token = token;
            RefreshToken = refreshToken;
        }
    }
}
