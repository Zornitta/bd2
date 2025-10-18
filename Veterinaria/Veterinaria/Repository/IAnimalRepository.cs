using Veterinaria.Models;

namespace Veterinaria.Repository
{
    public interface IAnimalRepository
    {
        public Task Create(Animal animal);
        public Task Update(Animal animal);
        public Task Delete(Animal animal);
        public Task<Animal?> GetById(int id);
        public Task<List<Animal>> GetAll();
        public Task<List<Animal>> GetByName(string name);
    }
}
