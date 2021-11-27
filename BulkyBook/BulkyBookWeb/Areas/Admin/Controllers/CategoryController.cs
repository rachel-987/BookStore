using BulkyBook.DataAccess.Repository.IRepository;
using BulkyBook.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace BulkyBookWeb.Controllers
{
    [Area("Admin")]
    public class CategoryController : Controller
    {
        private readonly IUnitOfWork unitOfWork;

        public CategoryController(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public IActionResult Index()
        {
            IEnumerable<Category> objectCategoryList = unitOfWork.CategoryRepository.GetAll(); ;
            return View(objectCategoryList);
        }

        //GET
        public IActionResult Create()
        {
            return View();
        }

        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Category category)
        {
            if (category.Name == category.DisplayOrder.ToString())
            {
                ModelState.AddModelError("name", "The display order cannot exactly match the Name.");
            }
            if (unitOfWork.CategoryRepository.GetFirstOrDefault(p => p.Name == category.Name) != null)
            {
                ModelState.AddModelError("name", "This category has existed.");
            }
            if (ModelState.IsValid)
            {
                unitOfWork.CategoryRepository.Add(category);
                unitOfWork.Save();
                TempData["Success"] = "Category added successfully";
                return RedirectToAction("Index");
            }
            return View(category);
        }

        //GET
        /// <summary>
        /// Update category
        /// </summary>
        /// <param name="id">Category ID</param>
        /// <returns></returns>
        public IActionResult Update(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var categoryFromDb = unitOfWork.CategoryRepository.GetFirstOrDefault(p => p.ID == id);

            if (categoryFromDb == null)
            {
                return NotFound();
            }

            return View(categoryFromDb);
        }

        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Update(Category category)
        {
            if (category.Name == category.DisplayOrder.ToString())
            {
                ModelState.AddModelError("name", "The display order cannot exactly match the Name.");
            }
            if (ModelState.IsValid)
            {
                unitOfWork.CategoryRepository.Update(category);
                unitOfWork.Save();
                TempData["Success"] = "Category updated successfully";
                return RedirectToAction("Index");
            }
            return View(category);
        }

        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var categoryFromDb = unitOfWork.CategoryRepository.GetFirstOrDefault(p => p.ID == id);

            if (categoryFromDb == null)
            {
                return NotFound();
            }

            return View(categoryFromDb);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(Category category)
        {
            unitOfWork.CategoryRepository.Remove(category);
            unitOfWork.Save();
            TempData["Success"] = "Category removed successfully";
            return RedirectToAction("Index");
        }
    }
}