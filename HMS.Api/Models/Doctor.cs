using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HMS.Api.Models
{
    public class Doctor
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }

        public string? Specialization { get; set; }

        public string? Qualification { get; set; }

        public int ExperienceYears { get; set; }

        public string? Phones { get; set; }

        public int DepartmentId { get; set; }

        [ForeignKey("DepartmentId")]
        public Department? Department { get; set; }
    }
}