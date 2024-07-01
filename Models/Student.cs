namespace QuanLyThiHUMG.Models
{
    public class Student
    {
        public required string StudentCode { get; set; }
        public required string LastName { get; set; }

        public required string FirstName { get; set; }

        public required string BirthDay { get; set; }
        public bool IsActive { get; set; } = true;
        public bool IsDelete { get; set; } = false;
    }
}