namespace DomainLayer.Auth
{
    public class AuthenticateRequest
    {
        public required string Lietotajvards { get; set; }
        public required string Parole { get; set; }
    }
}
