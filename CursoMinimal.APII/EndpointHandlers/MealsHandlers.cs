using AutoMapper;
using CursoMinimal.API.DBContexts;
using CursoMinimal.API.Entities;
using CursoMinimal.API.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CursoMinimal.API.EndpointHandlers
{
    public static class MealsHandlers
    {
        public static async Task<Results<NoContent, Ok<IEnumerable<MealModel>>>> GetMealsAsync(
            MealDbContext dbContext,
            IMapper mapper,
            ILogger<MealModel> logger,
            [FromQuery(Name = "name")] string? mealName)
        {
            var mealsEntity = await dbContext.Meals.Where(x => mealName == null || x.Name.ToLower().Contains(mealName.ToLower())).ToListAsync();

            if (mealsEntity == null)
            {
                logger.LogInformation("meal not found");
                return TypedResults.NoContent();
            }
            else
            {
                logger.LogInformation("return meal found");
                return TypedResults.Ok(mapper.Map<IEnumerable<MealModel>>(mealsEntity));
            }
        }

        public static async Task<CreatedAtRoute<MealModel>> PostMealsAsync(MealDbContext dbContext, IMapper mapper, [FromBody] RequestMealModel? requestMeal)
        {
            var mealEntity = mapper.Map<Meal>(requestMeal);
            dbContext.Add(mealEntity);
            await dbContext.SaveChangesAsync();

            var mealReturn = mapper.Map<MealModel>(mealEntity);

            return TypedResults.CreatedAtRoute(mealReturn, "GetMeals", new
            {
                mealsId = mealReturn.Id
            });
        }

        public static async Task<Results<NoContent, Ok<MealModel>>> GetMealsByIdAsync(MealDbContext dbContext, IMapper mapper, int mealsId)
        {
            var mealsEntity = mapper.Map<MealModel>(await dbContext.Meals.FirstOrDefaultAsync(x => x.Id == mealsId));
            if (mealsEntity == null)
                return TypedResults.NoContent();
            else
                return TypedResults.Ok(mealsEntity);
        }

        public static async Task<Results<NotFound, CreatedAtRoute<MealModel>>> PutMealsAsync(MealDbContext dbContext, IMapper mapper, int mealsId, [FromBody] RequestMealModel? requestMeal)
        {
            var mealEntity = await dbContext.Meals.FirstOrDefaultAsync(x => x.Id == mealsId);
            if (mealEntity == null)
                return TypedResults.NotFound();

            mapper.Map(requestMeal, mealEntity);

            await dbContext.SaveChangesAsync();

            var mealReturn = mapper.Map<MealModel>(mealEntity);

            return TypedResults.CreatedAtRoute(mealReturn, "GetMeals", new
            {
                mealsId = mealReturn.Id
            });
        }

        public static async Task<Results<NotFound, NoContent>> DeleteMealsAsync(MealDbContext dbContext, int mealsId)
        {
            var mealEntity = await dbContext.Meals.FirstOrDefaultAsync(x => x.Id == mealsId);
            if (mealEntity == null)
                return TypedResults.NotFound();

            dbContext.Remove(mealEntity);
            await dbContext.SaveChangesAsync();


            return TypedResults.NoContent();
        }
    }
}
