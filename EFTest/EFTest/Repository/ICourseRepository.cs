using EFTest.Models;

namespace EFTest.Repository
{
    public interface ICourseRepository
    {
        public Task Create(Course student);
        public Task Update(Course student);
        public Task Delete(Course student);
        public Task<Course?> GetById(int id);
        public Task<List<Course>> GetAll();
        public Task<List<Course>> GetByName(string name);
    }
}
