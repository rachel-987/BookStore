﻿using System.ComponentModel.DataAnnotations;

namespace BulkyBook.Models
{
    public class CoverType
    {
        [Key]
        public int ID { get; set; }

        [Required]
        public string Name { get; set; }
    }
}