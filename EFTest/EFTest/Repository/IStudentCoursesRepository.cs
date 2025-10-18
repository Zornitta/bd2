using EFTest.Models;

namespace EFTest.Repository
{
    public interface IStudentCoursesRepository
    {
        public Task Create(StudentCourses studentcourses);
        public Task Update(StudentCourses studentcourses);
        public Task Delete(StudentCourses studentcourses);
        public Task<List<StudentCourses?>> GetByStudentId(int studentId);
        public Task<List<StudentCourses?>> GetByCourseId(int courseId);
        public Task<List<StudentCourses?>> Get(int studentId, int courseId);
        public Task<List<StudentCourses>> GetAll();
        public Task<List<StudentCourses>> GetByCourseName(string name);
        public Task<List<StudentCourses>> GetByStudentName(string name);
    }
}
