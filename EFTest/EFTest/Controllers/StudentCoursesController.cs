using EFTest.Models;
using EFTest.Repository;
using EFTest.ViewModels.StudentCourses;
using Microsoft.AspNetCore.Mvc;

namespace EFTest.Controllers
{
    public class StudentCoursesController : Controller
    {
        private readonly ICourseRepository _courseRepository;
        private readonly IStudentRepository _studentRepository;
        private readonly IStudentCoursesRepository _studentCoursesRepository;

        public StudentCoursesController
            (ILogger<CourseController> logger,
            ICourseRepository courseRepository,
            IStudentRepository studentRepository,
            IStudentCoursesRepository studentCoursesRepository
        )
        {
            _courseRepository = courseRepository;
            _studentRepository = studentRepository;
            _studentCoursesRepository = studentCoursesRepository;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var data = await _studentRepository.GetAll();

            return View(data);
        }

        [HttpGet]

        public async Task<IActionResult> Create()
        {
            var viewModel = new StudentCoursesViewModel();

            viewModel.Students = await _studentRepository.GetAllNotEnrolled();

            viewModel.SetCourses(await _courseRepository.GetAll());

            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Create(StudentCoursesViewModel viewModel)
        {
            if(ModelState.IsValid)
            {
                foreach (var c in viewModel.Courses)
                {
                    if(c.IsSelected)
                    {
                        await _studentCoursesRepository.Create(
                            new Models.StudentCourses {
                                StudentID = viewModel.StudentId,
                                CourseID = c.Id,
                                SignDate = DateTime.Now
                            }
                        );
                    }
                }

                return RedirectToAction("Index");
            }
            return View(viewModel);

        }

        [HttpGet]
        public async Task<IActionResult> Update(int studentId)
        {
            var allCourses = await _courseRepository.GetAll();
            var enrolledCourses = await _studentCoursesRepository.GetByStudentId(studentId);

            var student = await _studentRepository.GetById(studentId);

            var viewModel = new StudentCoursesViewModel
            {
                StudentId = studentId,
                StudentName = student != null ? $"{student.FirstMidName} {student.LastName}" : "",
                Students = null
            };
            viewModel.SetCourses(allCourses);

            foreach (var c in viewModel.Courses)
            {
                // Marque como selecionado se o estudante está matriculado e não cancelado
                c.IsSelected = enrolledCourses.Any(
                    sc => sc != null && sc.CourseID == c.Id && sc.CancelDate == null
                );
            }
            return View(viewModel);

        }

        [HttpPost]

        public async Task<IActionResult> Update(StudentCoursesViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var enrolledCourses = await _studentCoursesRepository.GetByStudentId(viewModel.StudentId);

                foreach (var c in viewModel.Courses)
                {
                    var isEnrolled = enrolledCourses.FirstOrDefault(
                        sc => sc != null && 
                        sc.CourseID == c.Id &&
                        sc.CancelDate == null
                    );

                    if (c.IsSelected)
                    {
                        if (isEnrolled == null)
                        {
                            await _studentCoursesRepository.Create(
                                new Models.StudentCourses
                                {
                                    StudentID = viewModel.StudentId,
                                    CourseID = c.Id,
                                    SignDate = DateTime.Now
                                }
                            );
                        }
                    }
                    else
                    {
                        if (isEnrolled != null)
                        {
                            isEnrolled.CancelDate = DateTime.Now;
                            await _studentCoursesRepository.Update(isEnrolled);
                        }
                    }
                }
                return RedirectToAction("Index");
            }
            viewModel.SetCourses(await _courseRepository.GetAll());
            return View(viewModel);
        }
    }
}
