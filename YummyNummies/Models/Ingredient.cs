using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace YummyNummies.Models
{
    public class Ingredient
    {
        // Global Alias
        [Display(Name = "Ingredient Id")]
        public int IngredientId { get; set; }

        //Name Conditions: no empty strings, max # of chars=100
        [Required(AllowEmptyStrings = false, ErrorMessage = "Please enter an ingredient name")]
        [MaxLength(100)]
        public string Name { get; set; }

        //Table References
        //Reference to RecipeIngredients table (child class) 
        public List<RecipeIngredient> RecipeIngredients { get; set; }
    }
}

