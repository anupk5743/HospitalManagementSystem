using System.ComponentModel.DataAnnotations;

namespace HMS.Web.Models
{
    public class AppointmentVM
    {
        public int Id { get; set; }

        public int PatientId { get; set; }

        [Display(Name = "Patient Name")]
        public string? PatientName { get; set; }

        public int DoctorId { get; set; }

        [Display(Name = "Doctor Name")]
        public string? DoctorName { get; set; }

        [Display(Name = "Department")]
        public string? DepartmentName { get; set; }

        [Display(Name = "Appointment Date")]
        public DateTime AppointmentDate { get; set; }

        public string? Symptoms { get; set; }

        public string? Status { get; set; }
    }
}