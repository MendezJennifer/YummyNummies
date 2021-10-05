using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace YummyNummies.Models
{
    public class RecipeIngredient
    {
        // Global Alias
        [Display(Name = "Recipe Ingredient Id")]
        public int RecipeIngredientId { get; set; }

        //Foreign key
        [Required]
        public int IngredientId { get; set; }

        //Quantity Conditions: required, quantity between 0-100
        [Required]
        [Range(0.1, 100)]
        public double Quantity { get; set; }

        //Foreign key
        [Required]
        public int RecipeId { get; set; }

        //Table References
        //Reference to Recipe Table (parent class)
        public Recipe Recipe { get; set; }

        //Reference to Ingredients table (parent classs) 
        public Ingredient Ingredient { get; set; }
    }
}

