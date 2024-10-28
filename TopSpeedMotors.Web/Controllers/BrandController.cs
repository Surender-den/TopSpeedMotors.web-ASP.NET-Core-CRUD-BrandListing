using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using TopSpeedMotors.Web.Models;
using TopSpeedMotors.Web.NewFolder;

namespace TopSpeedMotors.Web.Controllers
{
    public class BrandController : Controller
    {
        private readonly ApplicationDBContext _dBContext;
        private readonly IWebHostEnvironment _webHostEnvironment;


        public BrandController(ApplicationDBContext dBContext, IWebHostEnvironment webHostEnvironment)
        {
            _dBContext = dBContext;
            _webHostEnvironment = webHostEnvironment;
        }
        [HttpGet]
        public IActionResult Index()

        {
            List<Brand> brands = _dBContext.Brand.ToList();
            return View(brands);
        }
        [HttpGet]
        public IActionResult Create()

        {

            return View();
        }
        [HttpPost]

        public IActionResult Create(Brand brand)

        {
            string webRootPath = _webHostEnvironment.WebRootPath;
            var file = HttpContext.Request.Form.Files;
            if (file.Count > 0)
            {
                string newFileName = Guid.NewGuid().ToString();
                var upload = Path.Combine(webRootPath, @"images\brand");
                var extension = Path.GetExtension(file[0].FileName);
                using (var fileStream = new FileStream(Path.Combine(upload, newFileName + extension), FileMode.Create))
                {
                    file[0].CopyTo(fileStream);
                    brand.BrandLogo = @"\images\brand\" + newFileName + extension;
                }
            }
                if (ModelState.IsValid)
                {
                    _dBContext.Brand.Add(brand);
                    _dBContext.SaveChanges();

                TempData["Success"] = "Record Created Successfully";
                return RedirectToAction(nameof(Index));

                }
                return View();
            }
        [HttpGet]
        public IActionResult Details(Guid id)
        {
            Brand? brand = _dBContext.Brand.FirstOrDefault(x => x.Id == id);

            return View(brand);
        }

        [HttpGet]
        public IActionResult Edit(Guid id)
        {
            Brand? brand = _dBContext.Brand.FirstOrDefault(x => x.Id == id);

            return View(brand);
        }

        [HttpPost]
        public IActionResult Edit(Brand brand)
        {

            string webRootPath = _webHostEnvironment.WebRootPath;
            var file = HttpContext.Request.Form.Files;
            if (file.Count > 0)
            {
                string newFileName = Guid.NewGuid().ToString();
                var upload = Path.Combine(webRootPath, @"images\brand");
                var extension = Path.GetExtension(file[0].FileName);
                // delete old image
                var objFromDb = _dBContext.Brand.AsNoTracking().FirstOrDefault(x => x.Id == brand.Id);
                if (objFromDb.BrandLogo != null)
                {
                    var oldImagePath = Path.Combine(webRootPath, objFromDb.BrandLogo.Trim('\\'));
                    if (System.IO.File.Exists(oldImagePath))
                    {
                        System.IO.File.Delete(oldImagePath);
                    } 
                }
                
               
                using (var fileStream = new FileStream(Path.Combine(upload, newFileName + extension), FileMode.Create))
                {
                    file[0].CopyTo(fileStream);
                    brand.BrandLogo = @"\images\brand\" + newFileName + extension;
                }
            }
            if (ModelState.IsValid)
            {
                _dBContext.Brand.Update(brand);                 
                _dBContext.SaveChanges();
                TempData["Warning"] = "Record Updated Successfully";

                return RedirectToAction(nameof(Index));
            }
            return View();
           
        }


        [HttpGet]
        public IActionResult Delete(Guid id)
        {
            Brand? brand = _dBContext.Brand.FirstOrDefault(x => x.Id == id);

            return View(brand);
        }

        [HttpPost]
        public IActionResult Delete(Brand brand)
        {
            String webRootPath = _webHostEnvironment.WebRootPath;

            if (!string.IsNullOrEmpty(brand.BrandLogo))
            {
                // delete old image
                var objFromDb = _dBContext.Brand.AsNoTracking().FirstOrDefault(x => x.Id == brand.Id);
                if (objFromDb.BrandLogo != null)
                {
                    var oldImagePath = Path.Combine(webRootPath, objFromDb.BrandLogo.Trim('\\'));
                    if (System.IO.File.Exists(oldImagePath))
                    {
                        System.IO.File.Delete(oldImagePath);
                    }
                }

            }
            _dBContext.Brand.Remove(brand);
            _dBContext.SaveChanges();
            TempData["Error"] = "Record Deleted Successfully";


            return RedirectToAction(nameof(Index));

        }
    }
    }



