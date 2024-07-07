using Microsoft.AspNetCore.Mvc;
using QuanLyThiHUMG.Data;
using Microsoft.EntityFrameworkCore;
using QuanLyThiHUMG.Models.Entities;
namespace QuanLyThiHUMG.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExamController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        public ExamController(ApplicationDbContext context)
        {
            _context = context;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Exam>>> GetExam()
        {
            return await _context.Exams.Where(m => m.IsDelete == false).ToListAsync();
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<Exam>> GetExam(int id)
        {
            var exam = await _context.Exams.FindAsync(id);
            if (exam == null)
            {
                return NotFound();
            }
            return exam;
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> PutExam(int id, Exam exam)
        {
            if (id != exam.ExamId)
            {
                return BadRequest();
            }
            _context.Entry(exam).State = EntityState.Modified;
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ExamExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return NoContent();
        }
        [HttpPost]
        public async Task<ActionResult<Exam>> PostExam(Exam exam)
        {
            exam.CreateDate = DateTime.Now;
            exam.CreatePerson = "Admin";
            exam.IsDelete = false;
            exam.Status = false;
            _context.Exams.Add(exam);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetExam", new { id = exam.ExamId }, exam);
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteExam(int id)
        {
            var exam = await _context.Exams.FindAsync(id);
            if (exam == null)
            {
                return NotFound();
            }
            var countStudentExam = _context.StudentExams.Where(x => x.ExamId == id)
                                    .Select(m => m.StudentExamID).Count();
            if (countStudentExam < 1) _context.Exams.Remove(exam);
            else
            {
                exam.IsDelete = true;
                _context.Entry(exam).State = EntityState.Modified;
            }
            await _context.SaveChangesAsync();

            return NoContent();
        }
        private bool ExamExists(int id)
        {
            return _context.Exams.Any(e => e.ExamId == id);
        }
    }
}