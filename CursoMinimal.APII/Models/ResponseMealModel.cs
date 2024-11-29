using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace CursoMinimal.API.Models
{
    public class ResponseMealModel
    {
        public int Id { get; set; }
        public required string Name { get; set; }
    }
}
