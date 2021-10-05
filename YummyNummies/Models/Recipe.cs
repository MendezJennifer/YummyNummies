using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace YummyNummies.Models
{
    public class Recipe
    {
        // Global Alias
        [Display(Name = "Recipe Id")]
        public int RecipeId { get; set; }

        //Name Conditions: no empty strings, max # of chars=500
        [Required(AllowEmptyStrings = false, ErrorMessage = "Please enter a recipe name")]
        [MaxLength(500)]
        public string Name { get; set; }

        //Rating Conditions: required, rating between 0-5
        [Required]
        [Range(0, 5)]
        public int Rating { get; set; }

        //FirstName Conditions: no empty strings
        [Required(AllowEmptyStrings = false, ErrorMessage = "Please enter the user's first name")]
        public string UserName { get; set; }

        //CookTime Conditions: required, rating between 0-9999
        [Required]
        [Range(0, 9999)]
        public int CookTime { get; set; }

        //Step Conditions: no empty strings, min # of chars=100
        [Required(AllowEmptyStrings = false, ErrorMessage = "Please enter the recipe steps")]
        [MinLength(100)]
        public string Steps { get; set; }

        public string Photo { get; set; }

        //Foreign key
        [Required]
        public string UserId { get; set; }

        //Foreign key
        [Required]
        public int CategoryId { get; set; }

        //Table References
        //Reference to Category Table (parent class)
        public Category Category { get; set; }

        //Reference to RecipeIngredients table (child class) 
        public List<RecipeIngredient> RecipeIngredients { get; set; }
    }
}

