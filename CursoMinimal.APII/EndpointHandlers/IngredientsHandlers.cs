using AutoMapper;
using CursoMinimal.API.DBContexts;
using CursoMinimal.API.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;

namespace CursoMinimal.API.EndpointHandlers
{
    public static class IngredientsHandlers
    {
        public static async Task<Results<NoContent, Ok<List<IngredientModel>>>> GetMealsIngredientsAsync(MealDbContext dbContext, IMapper mapper, int mealsId)
        {
            var ingredients = mapper.Map<IEnumerable<IngredientModel>>((await dbContext.Meals.Include(x => x.Ingredients).FirstOrDefaultAsync(x => x.Id == mealsId))?.Ingredients).ToList();

            if (ingredients.Count == 0 || ingredients == null)
                return TypedResults.NoContent();
            else
                return TypedResults.Ok(ingredients);
        }
    }
}
