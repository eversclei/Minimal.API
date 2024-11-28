using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace CursoMinimal.API.Models
{
    public class IngredientModel
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public int MealId { get; set; }
    }
}
