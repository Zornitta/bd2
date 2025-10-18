using EFTest.Data;
using EFTest.Models;
using Microsoft.EntityFrameworkCore;

namespace EFTest.Repository
{
    public class CourseRepository : ICourseRepository
    {
        private readonly SchoolContext _context;

        public CourseRepository(SchoolContext context)
        {
            _context = context;
        }

        public async Task Create(Course course)
        {
            await _context.Courses.AddAsync(course);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(Course course)
        {
            _context.Courses.Remove(course);
            await _context.SaveChangesAsync();
        }

        public async Task<List<Course>> GetAll()
        {
            var data = await _context.Courses.ToListAsync();
            return data;
        }

        public async Task<Course?> GetById(int id)
        {
            var course = await _context.Courses.Where(s => s.ID == id).FirstOrDefaultAsync();
            return course;
        }

        public async Task<List<Course>> GetByName(string name)
        {
            return await _context.Courses.Where(w => w.Name!.ToLower() ==name.ToLower()).ToListAsync();
        }

        public async Task Update(Course course)
        {
            _context.Courses.Update(course);
            await _context.SaveChangesAsync();
        }
    }
}
