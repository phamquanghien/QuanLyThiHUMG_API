using System.ComponentModel.DataAnnotations;

namespace QuanLyThiHUMG.Models.Entities
{
    public class Student
    {
        [Key]
        [MaxLength(50)]
        [Required]
        public string StudentCode { get; set; }
        [MaxLength(15)]
        public string? LastName { get; set; }

        public string? FirstName { get; set; }

        public string? BirthDay { get; set; }
        public bool IsActive { get; set; } = true;
        public bool IsDelete { get; set; } = false;
    }
}


