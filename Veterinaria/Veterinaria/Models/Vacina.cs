using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace Veterinaria.Models
{
    public class Vacina
    {
        [Key]
        public int Id { get; set; }
        public string? Nome { get; set; }
        public DateTime DataAplicacao { get; set; }
        public int AnimalId { get; set; }
        [ForeignKey(nameof(AnimalId))]
        public Animal? Animal { get; set; }
    }
}
