using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace CursoMinimal.API.Models
{
    public class RequestMealModel
    {
        public required string Name { get; set; }
    }
}
