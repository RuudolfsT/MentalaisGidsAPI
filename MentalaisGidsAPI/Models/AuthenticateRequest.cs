namespace MentalaisGidsAPI.Models
{
    public class AuthenticateRequest
    {
        public required string Username { get; set; }
        public required string Password { get; set; }
    }
}
