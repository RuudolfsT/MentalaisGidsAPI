namespace MentalaisGidsAPI.Domain.Dto
{
    public class NodarbibaDto
    {
        public int NodarbibaID { get; set; }
        public int SpecialistsID { get; set; }
        public int? LietotajsID { get; set; }
        public DateTime Sakums {  get; set; }
        public DateTime Beigas { get; set; }
    }
}