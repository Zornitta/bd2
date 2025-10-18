using EFTest.Data;
using EFTest.Models;
using Microsoft.EntityFrameworkCore;

namespace EFTest.Repository
{
    public class StudentRepository : IStudentRepository
    {
        private readonly SchoolContext? _context;
        public StudentRepository(SchoolContext? context)
        {
            _context = context;
        }
        public async Task Create(Student student)
        {
            await _context.Students.AddAsync(student);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(Student student)
        {
            _context.Students.Remove(student);
            await _context.SaveChangesAsync();
        }

        public async Task<List<Student>> GetAll()
        {
            var data = await _context.Students
                                     .Include(sc => sc.StudentCourses!)
                                     .ThenInclude(c => c.Course)
                                     .ToListAsync();
            return data;
        }

        public async Task<List<Student>> GetAllNotEnrolled()
        {
            var enrolledStudentIds = _context.StudentCourses
                                           .Select(sc => sc.StudentID)
                                           .Distinct();

            var data = await _context.Students
                                     .Include(sc => sc.StudentCourses!)
                                     .ThenInclude(c => c.Course)
                                     .Where(w => !enrolledStudentIds.Contains(w.ID))
                                     .OrderBy(s => s.FirstMidName)
                                     .ToListAsync();
            return data;
        }

        public async Task<Student?> GetById(int id)
        {
            var student = await _context.Students.Where(s => s.ID ==id).FirstOrDefaultAsync();
            return student;
        }

        public async Task<List<Student>> GetByName(string name)
        {
            var students = await _context.Students.Where(s => s.FirstMidName!.ToLower().Contains(name.ToLower())).ToListAsync();
            return students;
        }

        public async Task Update(Student student)
        {
            _context.Students.Update(student);
            _context.SaveChanges();
        }
    }
}
