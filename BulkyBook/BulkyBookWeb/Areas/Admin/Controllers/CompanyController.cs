using BulkyBook.DataAccess.Repository.IRepository;
using BulkyBook.Models;
using Microsoft.AspNetCore.Mvc;

namespace BulkyBookWeb.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CompanyController : Controller
    {
        private readonly IUnitOfWork unitOfWork;

        public CompanyController(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        // GET: CompanyController
        public ActionResult Index()
        {
            return View(/*companies*/);
        }

        #region API Calls - Getall, DeletePOST

        [HttpGet]
        public IActionResult GetAll()
        {
            var companies = unitOfWork.CompanyRepository.GetAll();
            return Json(new { data = companies });
        }

        [HttpDelete]
        public IActionResult Delete(int? id)
        {
            var company = unitOfWork.CompanyRepository.GetFirstOrDefault(p => p.ID == id);
            if (company == null)
            {
                return Json(new { success = false, message = "Error while deleting" });
            }

            unitOfWork.CompanyRepository.Remove(company);
            unitOfWork.Save();
            return Json(new { success = true, message = "Delete Successful" });
        }

        #endregion API Calls - Getall, DeletePOST

        #region Upsert Company <Update+Insert>
        public IActionResult Upsert(int? id)
        {
            Company company = new();
            if (id == null || id == 0)
            {
                ////Create product
                return View(company);
            }
            else
            {
                //Update Product
                company = unitOfWork.CompanyRepository.GetFirstOrDefault(p => p.ID == id);
                return View(company);
            }
        }


        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Upsert(Company company)
        {
            if (ModelState.IsValid)
            { 
                if (company.ID == 0)
                {
                    unitOfWork.CompanyRepository.Add(company);
                }
                else
                {
                    unitOfWork.CompanyRepository.Update(company);
                }
                //unitOfWork.ProductRepository.Add(productVm.Product);
                unitOfWork.Save();
                TempData["Success"] = "Company created successfully";
                return RedirectToAction("Index");
            }
            return View(company);
        }
        #endregion
    }
}