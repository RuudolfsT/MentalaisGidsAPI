namespace ServiceLayer
{
    // šajā struct var likt papildus lietotāja claim tipus, lai pie JWT token radīšanas, nolasīšanas,
    // neparādītos kaut kādi magic string
    public readonly struct CustomClaimTypes
    {
        public static readonly string UserId = "userid";
    }
}
