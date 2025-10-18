using Veterinaria.Data;
using Veterinaria.Models;
using Microsoft.EntityFrameworkCore;

namespace Veterinaria.Repository
{
    public class AnimalRepository : IAnimalRepository
    {
        private readonly VetContext? _context;
        public AnimalRepository(VetContext context)
        {
            _context = context;
        }
        public async Task Create(Animal animal)
        {
            await _context.Animais.AddAsync(animal);
            await _context.SaveChangesAsync();
        }
        public async Task Update(Animal animal)
        {
            _context.Animais.Update(animal);
            _context.SaveChanges();
        }
        public async Task Delete(Animal animal)
        {
            _context.Animais.Remove(animal);
            await _context.SaveChangesAsync();
        }
        public async Task<Animal?> GetById(int id)
        {
            return await _context.Animais
                .Include(a => a.TipoAnimal)
                .FirstOrDefaultAsync(a => a.Id == id);
        }
        public async Task<List<Animal>> GetAll()
        {
            return await _context.Animais
                .Include(a => a.TipoAnimal)
                .ToListAsync();
        }
        public async Task<List<Animal>> GetByName(string name)
        {
            return await _context.Animais
                .Include(a => a.TipoAnimal)
                .Where(a => a.Nome.Contains(name))
                .ToListAsync();
        }
    }
}
