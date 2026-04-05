using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace HMS.Web.Models
{
    public class DoctorVM
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public string Specialization { get; set; }

        public string Qualification { get; set; }

        public int ExperienceYears { get; set; }

        public string Phones { get; set; }

        [Required]
        public int DepartmentId { get; set; }

        public string? DepartmentName { get; set; }

        public IEnumerable<SelectListItem>? Departments { get; set; }
    }
}