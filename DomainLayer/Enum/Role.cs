namespace DomainLayer.Enum
{
    public enum Role
    {
        ParastaisLietotajs,
        Specialists,
        Admins
    }

    public static class RoleUtils
    {
        public const string ParastsLietotajs = "Parastais lietotājs";
        public const string Specialists = "Speciālists";
        public const string Admins = "Admins";
        public const string Visi = $"{ParastsLietotajs},{Specialists},{Admins}";
    }
}
