using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using Veterinaria.Data;
using Veterinaria.Models;
using Veterinaria.Repository;

namespace Veterinaria.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IAnimalRepository _animalRepository;

        public HomeController
            (ILogger<HomeController> logger,
            IAnimalRepository animalRepository
        )
        {
            _logger = logger;
            _animalRepository = animalRepository;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _animalRepository.GetAll());
        }

        [HttpPost]
        public async Task<IActionResult> Create(Animal animal)
        {
            if (ModelState.IsValid)
            {
                await _animalRepository.Create(animal);
                return RedirectToAction("Index");
            }
            return View(animal);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            var animal = await _animalRepository.GetById(id);
            if (animal == null)
            {
                return NotFound();
            }
            return View(animal);
        }

        [HttpPost]
        public async Task<IActionResult> Update(Animal animal)
        {
            if (ModelState.IsValid)
            {
                await _animalRepository.Update(animal);
                return RedirectToAction("Index");
            }
            return View(animal);
        }

        [HttpGet]
        public async Task<IActionResult> Update(int id)
        {
            var animal = await _animalRepository.GetById(id);
            if (animal == null)
            {
                return NotFound();
            }
            return View(animal);
        }
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
