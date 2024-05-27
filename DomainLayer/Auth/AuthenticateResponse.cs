namespace DomainLayer.Auth
{
    public class AuthenticateResponse
    {
        public int LietotajsID { get; set; }
        public required string Vards { get; set; }
        public required string Uzvards { get; set; }
        public required string Lietotajvards { get; set; }
        public required string Token { get; set; }
    }
}
