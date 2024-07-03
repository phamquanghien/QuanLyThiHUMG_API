using System.ComponentModel.DataAnnotations;
namespace QuanLyThiHUMG.Models.Entities
{
    public class Exam
    {
        [Key]
        public int ExamId { get; set; }
        [Required]
        public string ExamCode { get; set; }
        [Required]
        public string ExamName { get; set; }
        [DataType(DataType.Date)]
        public DateTime CreateDate { get; set; } = DateTime.Now;
        [Required]
        public string CreatePerson { get; set; }
        public string? Note { get; set; }
        public bool? IsDelete { get; set; } = false;
        public bool? Status { get; set; } = false;
        public int StartRegistrationCode { get; set; } = 10000;
        public bool IsAutoGenRegistrationCode { get; set; } = false;
    }
}