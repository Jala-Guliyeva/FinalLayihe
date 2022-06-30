using MedicioBackend.DAL;
using MedicioBackend.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace MedicioBackend.Controllers
{
    public class HomeController : Controller
    {
        private AppDbContext _context;

        public HomeController(AppDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            HomeVM homeVM = new HomeVM();
            homeVM.sliders=_context.Sliders.ToList();
            homeVM.doctors=_context.Doctors.ToList();
            homeVM.questions=_context.Questions.ToList();
            homeVM.bios=_context.Bios.FirstOrDefault();
            return View(homeVM);
        }
    }
}
