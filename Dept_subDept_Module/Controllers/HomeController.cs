using Dept_subDept_Module.Data;
using Dept_subDept_Module.Models;
using Dept_subDept_Module.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace Dept_subDept_Module.Controllers
{
    public class HomeController : Controller
    {

        private readonly ApplicationDbContext _context;
        private readonly FileUploader _fileUploader;

        public HomeController(ApplicationDbContext context, FileUploader fileUploader)
        {
            _context = context;
            _fileUploader = fileUploader;
        }
            
        public async Task<IActionResult> Index()
        {
            return View(await _context.Department.ToListAsync());
        }

        [HttpGet]
        public IActionResult CreateDepartment()
        {
            ViewData["DepartmentId"] = new SelectList(_context.Department, "Id", "Name").Prepend(new SelectListItem { Value = "0", Text = "--Select--" });
            return View();
        }

        [HttpPost]
        public IActionResult Create(Department department, IFormCollection data)
        {
            if (data.Files.Count > 0)
            {
                var logoFile = data.Files["logoFile"];
                if (logoFile != null && logoFile.Length > 0)
                {
                    department.Logo = _fileUploader.FilePath(logoFile);
                }
            }
            else
            {
                department.Logo = "No file";
            }

            _context.Add(department);
            _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> EditDepatment(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var department = await _context.Department.FindAsync(id);
            ViewData["DepartmentId"] = new SelectList(_context.Department, "Id", "Name", department.Parent_Id).Prepend(new SelectListItem { Value = "0", Text = "--Select--" });
            if (department == null)
            {
                return NotFound();
            }
            return View(department);
        }

        [HttpPost]
        public IActionResult Edit(int id, Department department, IFormCollection data)
        {
            var tbl_data = _context.Department.Where(x => x.Id == id).FirstOrDefault();

            if (tbl_data != null)
            {
                if (id != tbl_data.Id)
                {
                    return NotFound();
                }

                try
                {
                    if (data.Files.Count > 0)
                    {
                        var logoFile = data.Files["logoFile"];
                        if (logoFile != null && logoFile.Length > 0)
                        {
                            tbl_data.Logo = _fileUploader.FilePath(logoFile);
                        }
                    }
                    tbl_data.Name = department.Name;
                    tbl_data.Parent_Id = department.Parent_Id;

                    _context.Update(tbl_data);
                    _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DepartmentExists(department.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
            }
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> DeleteDepatment(int id)
        {
            var department = await _context.Department.FindAsync(id);

            _context.Department.Remove(department);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DepartmentExists(int id)
        {
            return _context.Department.Any(e => e.Id == id);
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

        public async Task<IActionResult> ViewDepartment()
        {
            return View(await _context.Department.ToListAsync());
        }
    }
}
