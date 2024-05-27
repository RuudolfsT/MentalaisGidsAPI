namespace DomainLayer.Auth
{
    public class RegisterRequest
    {
        public required string Vards { get; set; }
        public required string Uzvards { get; set; }
        public int? Dzimums { get; set; }
        public required string Lietotajvards { get; set; }
        public required string Parole { get; set; }
        public required string Epasts { get; set; }
    }
}
