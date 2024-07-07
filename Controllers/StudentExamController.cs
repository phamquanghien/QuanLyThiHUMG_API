using System.Data;
using EFCore.BulkExtensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QuanLyThiHUMG.Data;
using QuanLyThiHUMG.Models.Entities;
using QuanLyThiHUMG.Models.Process;
namespace QuanLyThiHUMG.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentExamController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private ExcelProcess _excelProcess = new ExcelProcess();
        private StudentProcess _studentProcess = new StudentProcess();
        private SubjectProcess _subjectProcess = new SubjectProcess();
        public StudentExamController(ApplicationDbContext context)
        {
            _context = context;
        }
        // GET: api/StudentExam/5
        [HttpGet("count/{id}")]
        public async Task<ActionResult<StudentExam>> CountStudentExam(int id)
        {
            var countStudentExam = await _context.StudentExams.Where(m => m.ExamId == id).CountAsync();
            return Ok(countStudentExam);
        }
        [HttpPost("upload")]
        public async Task<IActionResult> Upload(IFormFile file, int examId)
        {
            if (file == null || file.Length == 0)
                return BadRequest("File tải lên không được để trống");
            if (Path.GetExtension(file.FileName).ToLower() != ".xlsx" && Path.GetExtension(file.FileName).ToLower() != ".xls")
            {
                return BadRequest("Vui lòng tải lên file Excel!");
            }
            try
            {
                string uploadsFolder = Path.Combine("Uploads", "Excels");
                string uniqueFileName = "File" + Guid.NewGuid().ToString() + "_" + file.FileName;
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                Directory.CreateDirectory(uploadsFolder);

                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await file.CopyToAsync(fileStream);
                    var fileLocation = new FileInfo(filePath).ToString();
                    var result = await ImportDataFromExcel(examId, fileLocation);
                    return Ok(result);
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex}");
            }
        }
        private async Task<string> ImportDataFromExcel(int examId, string fileLocation)
        {
            string messageResult = "";
            var dataFromExcel = _excelProcess.ExcelToDataTable(fileLocation);
            var studentDataTable = _studentProcess.GetStudentTableDistinct(dataFromExcel);
            var subjectDataTable = _subjectProcess.GetSubjectTableDistinct(dataFromExcel);
            if (studentDataTable.Rows.Count > 0)
            {
                var existingStudentCodes = await _context.Students.Select(m => m.StudentCode).ToListAsync();
                var existingSubjectCodes = await _context.Subjects.Select(m => m.SubjectCode).ToListAsync();
                try
                {
                    var newStudents = studentDataTable.AsEnumerable()
                                    .Where(row => !existingStudentCodes.Contains(row.Field<string>(0)))
                                    .Select(row => new Student
                                    {
                                        StudentCode = row.Field<string>(0),
                                        FirstName = row.Field<string>(1),
                                        LastName = row.Field<string>(2),
                                        BirthDay = row.Field<string>(3)
                                    });
                    await _context.Students.AddRangeAsync(newStudents);
                    var newSubjects = subjectDataTable.AsEnumerable()
                                    .Where(row => !existingSubjectCodes.Contains(row.Field<string>(0)))
                                    .Select(row => new Subject
                                    {
                                        SubjectCode = row.Field<string>(0),
                                        SubjectName = row.Field<string>(1)
                                    });
                    await _context.Subjects.AddRangeAsync(newSubjects);
                    var newStudentExams = dataFromExcel.AsEnumerable()
                                        .Select(row => new StudentExam
                                        {
                                            StudentCode = row.Field<string>(0),
                                            SubjectCode = row.Field<string>(1),
                                            IdentificationNumber = row.Field<string>(5),
                                            ClassName = row.Field<string>(6),
                                            TestDay = row.Field<string>(8),
                                            TestRoom = row.Field<string>(9),
                                            LessonStart = row.Field<string>(10),
                                            LessonNumber = row.Field<string>(11),
                                            ExamBag = Convert.ToInt32(row.Field<string>(12)),
                                            ExamId = examId
                                        });
                    await _context.StudentExams.AddRangeAsync(newStudentExams);
                    await _context.BulkSaveChangesAsync();
                    messageResult = "Import thành công " + dataFromExcel.Rows.Count + " sinh viên";
                }
                catch
                {
                    messageResult = "Dữ liệu bị lỗi, vui lòng kiểm tra lại dữ liệu file excel!";
                }
            }
            else
            {
                messageResult = "File Excel không có dữ liệu hoặc sai định dạng, vui lòng kiểm tra lại dữ liệu file excel!";
            }
            return messageResult;
        }
    }
}