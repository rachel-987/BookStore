using BulkyBook.DataAccess.Repository.IRepository;
using BulkyBook.Models.ViewModels;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.IO;
using System.Linq;

namespace BulkyBookWeb.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductController : Controller
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IWebHostEnvironment webHostEnvironment;

        public ProductController(IUnitOfWork unitOfWork, IWebHostEnvironment webHostEnvironment)
        {
            this.unitOfWork = unitOfWork;
            this.webHostEnvironment = webHostEnvironment;
        }

        public IActionResult Index()
        {
            //IEnumerable<Product> products = unitOfWork.ProductRepository.GetAll();
            return View(/*products*/);
        }

        #region Upsert Product <Update+Insert>

        //GET
        /// <summary>
        /// If id hasn't existed => Create new product
        /// else => Update product
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public IActionResult Upsert(int? id)
        {
            ProductVm productVm = new()
            {
                Product = new(),
                CategoryList = unitOfWork.CategoryRepository.GetAll().Select(i => new SelectListItem
                {
                    Text = i.Name,
                    Value = i.ID.ToString()
                }),
                CoverTypeList = unitOfWork.CoverTypeRepository.GetAll().Select(i => new SelectListItem
                {
                    Text = i.Name,
                    Value = i.ID.ToString()
                })
            };

            if (id == null || id == 0)
            {
                ////Create product
                return View(productVm);
            }
            else
            {
                //Update Product
                productVm.Product = unitOfWork.ProductRepository.GetFirstOrDefault(p => p.ID == id);
                return View(productVm);
            }
        }

        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Upsert(ProductVm productVm, IFormFile file)
        {
            if (ModelState.IsValid)
            {
                string wwwRootPath = webHostEnvironment.WebRootPath;
                if (file != null)
                {
                    string fileName = Guid.NewGuid().ToString();
                    var uploads = Path.Combine(wwwRootPath, @"images\products");
                    var extension = Path.GetExtension(file.FileName);
                    if (productVm.Product.ImageURL != null)
                    {
                        var oldImagePath = Path.Combine(wwwRootPath, productVm.Product.ImageURL.TrimStart('\\'));
                        if (System.IO.File.Exists(oldImagePath))
                        {
                            System.IO.File.Delete(oldImagePath);
                        }
                    }

                    using (var fileStreams = new FileStream(Path.Combine(uploads, fileName + extension), FileMode.Create))
                    {
                        file.CopyTo(fileStreams);
                    }
                    productVm.Product.ImageURL = @"\images\products\" + fileName + extension;
                }

                if (productVm.Product.ID == 0)
                {
                    unitOfWork.ProductRepository.Add(productVm.Product);
                }
                else
                {
                    unitOfWork.ProductRepository.Update(productVm.Product);
                }
                //unitOfWork.ProductRepository.Add(productVm.Product);
                unitOfWork.Save();
                TempData["Success"] = "Category created successfully";
                return RedirectToAction("Index");
            }
            return View(productVm);
        }

        #endregion Upsert Product <Update+Insert>

        #region API Calls - Getall, DeletePOST

        /// <summary>
        /// Get all data and return as JSON data
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult GetAll()
        {
            var productList = unitOfWork.ProductRepository.GetAll(includeProperties: "Category,CoverType");
            return Json(new { data = productList });
        }

        [HttpDelete]
        //[ValidateAntiForgeryToken] -- Nếu có cái này sẽ không xóa được T.T
        public IActionResult Delete(int? id)
        {
            var product = unitOfWork.ProductRepository.GetFirstOrDefault(p => p.ID == id);
            if (product == null)
            {
                return Json(new { success = false, message = "Error while deleting" });
            }

            var oldImagePath = Path.Combine(webHostEnvironment.WebRootPath, product.ImageURL.TrimStart('\\'));
            if (System.IO.File.Exists(oldImagePath))
            {
                System.IO.File.Delete(oldImagePath);
            }
            unitOfWork.ProductRepository.Remove(product);
            unitOfWork.Save(); 
            return Json(new { success = true, message = "Delete Successful" });
        }

        #endregion API Calls - Getall, DeletePOST
    }
}