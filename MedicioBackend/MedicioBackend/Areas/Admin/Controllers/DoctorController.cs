using MedicioBackend.DAL;
using MedicioBackend.Extentions;
using MedicioBackend.Helpers;
using MedicioBackend.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MedicioBackend.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class DoctorController : Controller
    {
        private AppDbContext _context;
        private IWebHostEnvironment _env;

        public DoctorController(AppDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }

        public IActionResult Index()
        {
            List<Doctor> doctor = _context.Doctors.ToList();
            return View(doctor);
        }

        public async Task<IActionResult> Detail(int? id)
        {
            if (id == null) return NotFound();
            Doctor dbDoctor=await _context.Doctors.FindAsync(id);
            if(dbDoctor == null) return NotFound(); 
            return View(dbDoctor);
        }

        public async Task<IActionResult> Delete(int? id)
        {

            if (id == null) return NotFound();
            Doctor dbDoctor = await _context.Doctors.FindAsync(id);
            if (dbDoctor == null) return NotFound();
            Helper.DeleteFile(_env, "assets/images",dbDoctor.Image);
            _context.Doctors.Remove(dbDoctor);
            await _context.SaveChangesAsync();  
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Update(int? id)
        {
            if (id == null) return NotFound();
            Doctor dbDoctor = await _context.Doctors.FindAsync(id);
            if (dbDoctor == null) return NotFound();
            return View(dbDoctor);
        }

        [HttpPost]
        public async Task<IActionResult> Update(int?id,Doctor doctor)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            Doctor dbDoctor = await _context.Doctors.FindAsync(id);
            if (dbDoctor == null) return NotFound();

            dbDoctor.Image=doctor.Image;
            dbDoctor.Title = doctor.Title;
            dbDoctor.Desc = doctor.Desc;

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
            
        }

        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult>Create(Doctor doctor)
        {
            if (ModelState["Photo"].ValidationState==Microsoft.AspNetCore.Mvc.ModelBinding.ModelValidationState.Invalid)
            {
                return View();
            }

            if (!doctor.Photo.IsImage())
            {
                ModelState.AddModelError("Photo", "formati image olmalidi");
                return View();
            }

            if (doctor.Photo.ImageSize(10000))
            {
                ModelState.AddModelError("Photo", "1mq cox ola bilmez");
                return View();
            }

            string fileName = await doctor.Photo.SaveImage(_env, "assets/images");
            Doctor newDoctor = new Doctor();
            newDoctor.Image = fileName;
            newDoctor.Title = doctor.Title;
            newDoctor.Desc = doctor.Desc;   
            await _context.AddAsync(newDoctor );
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
