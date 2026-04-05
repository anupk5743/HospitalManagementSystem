using HMS.Api.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HMS.API.Models
{
    public class Appointment
    {
        public int Id { get; set; }

        // Patient Foreign Key
        public int PatientId { get; set; }

        [ForeignKey("PatientId")]
        public Patient? Patient { get; set; }

        // Doctor Foreign Key
        public int DoctorId { get; set; }

        [ForeignKey("DoctorId")]
        public Doctor? Doctor { get; set; }

        [Required]
        public DateTime AppointmentDate { get; set; }

        public string? Symptoms { get; set; }

        public string Status { get; set; } = "Pending";
    }
}