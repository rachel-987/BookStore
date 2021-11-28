using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace BulkyBook.Models.ViewModels
{
    public class ProductVm
    {
        public Product Product { get; set; }

        //Get all category and display as dropdown
        [ValidateNever]
        public IEnumerable<SelectListItem> CategoryList { get; set; }

        //Get all covertype and display as dropdown
        [ValidateNever]
        public IEnumerable<SelectListItem> CoverTypeList { get; set; }
    }
}