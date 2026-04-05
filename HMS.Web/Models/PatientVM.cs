using System.ComponentModel.DataAnnotations;

namespace HMS.Web.Models
{
    public class PatientVM
    {
        public int Id { get; set; }

        [Required]
        public string FullName { get; set; }

        [Range(0, 120)]
        public int Age { get; set; }

        public string? Gender { get; set; }

        [Phone]
        public string? Phone { get; set; }

        public string? Email { get; set; }

        public string? BloodGroup { get; set; }

        public string? Address { get; set; }

        public string? EmergencyContact { get; set; }

        public string? MedicalHistory { get; set; }
    }
}