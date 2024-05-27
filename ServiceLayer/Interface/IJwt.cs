using MentalaisGidsAPI.Domain;

namespace ServiceLayer.Interface
{
    public interface IJwt
    {
        public string GenerateToken(Lietotajs lietotajs);
        public int? ValidateToken(string token);
    }
}
