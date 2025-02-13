namespace Selu383.SP25.Api.DTOs
{
    public class TheaterGetDto
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Address { get; set; }
        public int SeatCount { get; set; }
    }

    public class TheaterCreateDto
    {
        public string? Name { get; set; }
        public string? Address { get; set; }
        public int? SeatCount { get; set; }  // ✅ Changed to nullable
    }

    public class TheaterUpdateDto
    {
        public string? Name { get; set; }
        public string? Address { get; set; }
        public int? SeatCount { get; set; }  // ✅ Changed to nullable
    }
}
