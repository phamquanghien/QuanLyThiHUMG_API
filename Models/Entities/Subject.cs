using System.ComponentModel.DataAnnotations;

namespace QuanLyThiHUMG.Models.Entities
{
    public class Subject
    {
        [Key]
        [Required]
        public string SubjectCode { get; set; }
        [Required]
        public string SubjectName { get; set; }
        public bool IsActive { get; set; } = true;
        public bool IsDelete { get; set; } = false;
    }
}