﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BulkyBook.Models
{
    public class Category
    { 
        [Key]
        public int ID { get; set; }
        
        [Required]
        [DisplayName("Category Name")]
        public string Name { get; set; }

        [DisplayName("Display Order")]
        [Range(1,100, ErrorMessage ="Display Order must be between 1 and 100 only!!!")]
        public int DisplayOrder { get; set; }
        
        public DateTime CreatedDateTime { get; set; } = DateTime.Now;
    }
}
