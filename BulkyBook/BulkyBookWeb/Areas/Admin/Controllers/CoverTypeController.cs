using BulkyBook.DataAccess.Repository.IRepository;
using BulkyBook.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace BulkyBookWeb.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CoverTypeController : Controller
    {
        private readonly IUnitOfWork unitOfWork;

        public CoverTypeController(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public IActionResult Index()
        {
            IEnumerable<CoverType> coverTypes = unitOfWork.CoverTypeRepository.GetAll();
            return View(coverTypes);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(CoverType coverType)
        {
            if (unitOfWork.CoverTypeRepository.GetFirstOrDefault(p => p.Name == coverType.Name) != null)
            {
                ModelState.AddModelError("name", "This covertype has existed.");
            }
            if (ModelState.IsValid)
            {
                unitOfWork.CoverTypeRepository.Add(coverType);
                unitOfWork.Save();
                TempData["Success"] = "CoverType added successfully";
                return RedirectToAction("Index");
            }
            return View(coverType);
        }

        public IActionResult Update(int? id)
        {
            if (id==null||id==0)
            {
                return NotFound();
            }
            var coverType = unitOfWork.CoverTypeRepository.GetFirstOrDefault(p => p.ID == id);
            if (coverType == null)
            {
                return NotFound();
            }
            return View(coverType);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Update(CoverType coverType)
        {
            if (ModelState.IsValid)
            {
                unitOfWork.CoverTypeRepository.Update(coverType);
                unitOfWork.Save();
                TempData["Success"] = "CoverType updated successfully";
                return RedirectToAction("Index");
            }
            return View(coverType);
        }

        /// <summary>
        /// Delete gần giống Update
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var coverType = unitOfWork.CoverTypeRepository.GetFirstOrDefault(p => p.ID == id);
            if (coverType == null)
            {
                return NotFound();
            }
            return View(coverType);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(CoverType coverType)
        {
            unitOfWork.CoverTypeRepository.Remove(coverType);
            unitOfWork.Save();
            TempData["Success"] = "CoverType removed successfully";
            return RedirectToAction("Index"); 
        }
    }
}