using Veterinaria.Models;
namespace Veterinaria.Data
{
    public class DbInitializer
    {
        public static void Initialize(VetContext context)
        {
            context.Database.EnsureCreated();

            if (context.TiposAnimais.Any())
            {
                return;
            }

            var tiposAnimais = new TipoAnimal[]
            {
                new TipoAnimal { Especie = "Cachorro", Descricao = "Animal de estimação popular" },
                new TipoAnimal { Especie = "Gato", Descricao = "Outro animal de estimação comum" },
                new TipoAnimal { Especie = "Pássaro", Descricao = "Ave doméstica" },
                new TipoAnimal { Especie = "Coelho", Descricao = "Pequeno mamífero doméstico" },
                new TipoAnimal { Especie = "Hamster", Descricao = "Roedor de estimação" }
            };
            foreach (var tipo in tiposAnimais)
            {
                context.TiposAnimais.Add(tipo);
            }
            context.SaveChanges();

            var animais = new Animal[]
            {
                new Animal { Nome = "Rex", TipoAnimal = tiposAnimais[0], DtNascimento = new DateTime(2020, 1, 1), Raca = "Labrador", Cor = "Amarelo", Peso = 30.5f, Altura = 60.0f },
                new Animal { Nome = "Mia", TipoAnimal = tiposAnimais[1], DtNascimento = new DateTime(2019, 5, 15), Raca = "Siamês", Cor = "Cinza", Peso = 4.5f, Altura = 25.0f },
                new Animal { Nome = "Tico", TipoAnimal = tiposAnimais[2], DtNascimento = new DateTime(2021, 3, 10), Raca = "Canário", Cor = "Amarelo", Peso = 0.03f, Altura = 10.0f },
                new Animal { Nome = "Bunny", TipoAnimal = tiposAnimais[3], DtNascimento = new DateTime(2022, 7, 20), Raca = "Mini Lop", Cor = "Branco", Peso = 2.1f, Altura = 20.0f },
                new Animal { Nome = "Bolt", TipoAnimal = tiposAnimais[0], DtNascimento = new DateTime(2021, 11, 5), Raca = "Border Collie", Cor = "Preto e Branco", Peso = 18.0f, Altura = 50.0f },
                new Animal { Nome = "Luna", TipoAnimal = tiposAnimais[1], DtNascimento = new DateTime(2020, 8, 30), Raca = "Persa", Cor = "Branco", Peso = 5.0f, Altura = 24.0f },
                new Animal { Nome = "Nina", TipoAnimal = tiposAnimais[4], DtNascimento = new DateTime(2023, 2, 14), Raca = "Sírio", Cor = "Dourado", Peso = 0.12f, Altura = 8.0f }
            };
            foreach (var animal in animais)
            {
                context.Animais.Add(animal);
            }
            context.SaveChanges();

            var vacinas = new Vacina[]
            {
                new Vacina { Nome = "Vacina Antirrábica", DataAplicacao = new DateTime(2023, 1, 10), AnimalId = animais[0].Id },
                new Vacina { Nome = "Vacina V8", DataAplicacao = new DateTime(2023, 2, 20), AnimalId = animais[0].Id },
                new Vacina { Nome = "Vacina V10", DataAplicacao = new DateTime(2023, 3, 15), AnimalId = animais[1].Id },
                new Vacina { Nome = "Vacina Gripe", DataAplicacao = new DateTime(2023, 4, 10), AnimalId = animais[2].Id },
                new Vacina { Nome = "Vacina Mixomavirose", DataAplicacao = new DateTime(2023, 5, 5), AnimalId = animais[3].Id },
                new Vacina { Nome = "Vacina Cinomose", DataAplicacao = new DateTime(2023, 6, 12), AnimalId = animais[4].Id },
                new Vacina { Nome = "Vacina Leucemia Felina", DataAplicacao = new DateTime(2023, 7, 18), AnimalId = animais[5].Id }
            };
            foreach (var vacina in vacinas)
            {
                context.Vacinas.Add(vacina);
            }
            context.SaveChanges();
        }
    }
}
