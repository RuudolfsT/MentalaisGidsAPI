namespace MentalaisGidsAPI.Domain.dto
{
    public class RakstsDto
    {
        public int RakstsID { get; set; }
        public int SpecialistsID { get; set; }
        public string ? SpecialistsVards { get; set; }
        public string ? SpecialistsUzvards { get; set; }
        public string Virsraksts { get; set; }
        public string Saturs { get; set; }
        public DateTime DatumsUnLaiks { get; set; }
        public int ? Vertejums { get; set; }
        public List<KomentarsDto> Komentari { get; set; }
    }
}
