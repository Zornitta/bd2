using EFTest.Data;
using EFTest.Models;
using Microsoft.EntityFrameworkCore;

namespace EFTest.Repository
{
    public class StudentCoursesRepository : IStudentCoursesRepository
    {
        private readonly SchoolContext _context;

        public StudentCoursesRepository(SchoolContext? context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task Create(StudentCourses studentcourses)
        {
            await _context.StudentCourses.AddAsync(studentcourses);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(StudentCourses studentcourses)
        {
            _context.StudentCourses.Remove(studentcourses);
            await _context.SaveChangesAsync();
        }

        public async Task<List<StudentCourses?>> Get(int studentId, int courseId)
        {
            var data = await _context.StudentCourses
                .Include(x => x.Course)
                .Include(x => x.Student)
                .Where(w => w.StudentID == studentId && 
                            w.CourseID == courseId)
                .ToListAsync();
            return data.Cast<StudentCourses?>().ToList();
        }

        public async Task<List<StudentCourses>> GetAll()
        {
            var data = await _context.StudentCourses
                .Include(x => x.Course)
                .Include(x => x.Student)
                .ToListAsync();
            return data;
        }

        public async Task<List<StudentCourses?>> GetByCourseId(int courseId)
        {
            var data = await _context.StudentCourses
                .Include(x => x.Course)
                .Include(x => x.Student)
                .Where(w => w.CourseID == courseId)
                .ToListAsync();
            return data.Cast<StudentCourses?>().ToList();
        }

        public async Task<List<StudentCourses>> GetByCourseName(string name)
        {
            var data = await _context.StudentCourses
                .Include(x => x.Course)
                .Include(x => x.Student)
                .Where(w => w.Course!.Name!.ToLower().Contains(name.ToLower()))
                .ToListAsync();
            return data;
        }

        public async Task<List<StudentCourses?>> GetByStudentId(int studentId)
        {
            var data = await _context.StudentCourses
                .Include(x => x.Course)
                .Include(x => x.Student)
                .Where(w => w.StudentID == studentId)
                .ToListAsync();
            return data.Cast<StudentCourses?>().ToList();
        }

        public async Task<List<StudentCourses>> GetByStudentName(string name)
        {
            var data = await _context.StudentCourses
                .Include(x => x.Course)
                .Include(x => x.Student)
                .Where(w => w.Student!.FirstMidName!.ToLower().Contains(name.ToLower()) ||
                            w.Student!.LastName!.ToLower().Contains(name.ToLower()))
                .ToListAsync();
            return data;
        }

        public async Task Update(StudentCourses studentcourses)
        {
            _context.StudentCourses.Update(studentcourses);
            await _context.SaveChangesAsync();
        }
    }
}
