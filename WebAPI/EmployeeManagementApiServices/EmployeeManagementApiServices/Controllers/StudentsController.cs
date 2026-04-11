using EmployeeManagementApiServices.Data;
using EmployeeManagementApiServices.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EmployeeManagementApiServices.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public StudentsController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetStudents()
        {
            return Ok(await _context.Students.ToListAsync());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetStudent(int id)
        {
            var student = await _context.Students.FindAsync(id);
            if (student == null) return NotFound();
            return Ok(student);
        }

        [HttpPost]
        public async Task<IActionResult> CreateStudent(Student student)
        {
            student.Id = 0; // Reset ID to allow auto-increment
            _context.Students.Add(student);
            await _context.SaveChangesAsync();
            return Ok(student);
        }

        //[HttpPut("{id}")]
        //public async Task<IActionResult> UpdateStudent(int id, Student student)
        //{
        //    if (id != student.Id) return BadRequest();

        //    var existingStudent = await _context.Students.FindAsync(id);
        //    if (existingStudent == null) return NotFound();

        //    existingStudent.Name = student.Name;
        //    existingStudent.Age = student.Age;
        //    existingStudent.Course = student.Course;

        //    _context.Entry(existingStudent).State = EntityState.Modified;
        //    await _context.SaveChangesAsync();

        //    return Ok(existingStudent);
        //}

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateStudent(int id, Student student)
        {
            var existingStudent = await _context.Students.FindAsync(id);

            if (existingStudent == null)
                return NotFound();

            existingStudent.Name = student.Name;
            existingStudent.Age = student.Age;
            existingStudent.Course = student.Course;

            await _context.SaveChangesAsync();

            return Ok(existingStudent);
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteStudent(int id)
        {
            var student = await _context.Students.FindAsync(id);
            if (student == null) return NotFound();

            _context.Students.Remove(student);
            await _context.SaveChangesAsync();

            return Ok("Deleted");
        }
    }
}

