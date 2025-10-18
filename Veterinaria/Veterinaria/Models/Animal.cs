using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace Veterinaria.Models
{
    public class Animal
    {
        [Key]
        public int Id { get; set; }
        public string? Nome { get; set; }
        public int TipoAnimalId { get; set; }
        [ForeignKey(nameof(TipoAnimalId))]
        public TipoAnimal? TipoAnimal { get; set; }
        public DateTime DtNascimento { get; set; }
        public string? Raca { get; set; }
        public string? Cor { get; set; }
        public float Peso { get; set; }
        public float Altura { get; set; }
    }
}
