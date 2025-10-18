using System.Diagnostics;
using EFTest.Data;
using EFTest.Models;
using EFTest.Repository;
using Microsoft.AspNetCore.Mvc;

namespace EFTest.Controllers
{
    public class CourseController : Controller
    {
        private readonly ILogger<CourseController> _logger; // Variaveis privadas sempre tem _(underline) no inicio por convenção (boas práticas)
        private readonly ICourseRepository _courseRepository;
        public CourseController
            (ILogger<CourseController> logger,
            ICourseRepository courseRepository
        )
        {
            _logger = logger;
            _courseRepository = courseRepository;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _courseRepository.GetAll());
        }

        [HttpPost]
        public async Task<IActionResult> Create(Course course)
        {
            if (ModelState.IsValid)
            {
                await _courseRepository.Create(course);
                return RedirectToAction("Index");
            }
            return View(course);
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            var course = await _courseRepository.GetById(id);
            if(course == null)
            {
                return NotFound();
            }
            await _courseRepository.Delete(course);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> Update(Course course)
        {
            if (ModelState.IsValid)
            {
                await _courseRepository.Update(course);
                return RedirectToAction("Index");
            }
            return View(course);
        }

        [HttpGet]
        public async Task<IActionResult> Update(int id)
        {
            var course = await _courseRepository.GetById(id);
            if(course == null)
            {
                return NotFound();
            }
            return View(course);
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
