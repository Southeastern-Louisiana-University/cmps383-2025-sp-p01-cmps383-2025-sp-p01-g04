namespace Selu383.SP25.Api.Entities
{
    public class Theater
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public int SeatCount { get; set; }
    }
}
