using System.ComponentModel.DataAnnotations;
namespace HMS.Api.Models
{
    public class Department
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public string Description { get; set; }
        public string HeadOfDepartment { get; set; }
    }
}
