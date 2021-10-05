using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace YummyNummies.Models
{
    public class Category
    {
        // Global Alias
        [Display(Name = "Category Id")]
        public int CategoryId { get; set; }

        //Name Conditions: no empty strings, max # of chars=100
        [Required(AllowEmptyStrings = false, ErrorMessage = "Please write a category name")]
        [MaxLength(100)]
        public string Name { get; set; }

        //Table References
        //Reference to Recipes table (child class) 
        public List<Recipe> Recipes { get; set; }
    }
}

