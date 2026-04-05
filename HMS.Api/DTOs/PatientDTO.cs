namespace HMS.API.DTOs
{
    public class PatientDTO
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public int Age { get; set; }
        public string? Gender { get; set; }
        public string? Phone { get; set; }
    }
}