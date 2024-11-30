using CursoMinimal.API.EndpointFilters;
using CursoMinimal.API.EndpointHandlers;

namespace CursoMinimal.API.Extensions
{
    public static class EndpointRouteBuilderExtensions
    {
        public static void RegisterMealsEndpoints(this IEndpointRouteBuilder endpoints)
        {
            var mealsEndpoints = endpoints.MapGroup("/meals");
            var mealsParamEndpoints = mealsEndpoints.MapGroup("/{mealsId:int}");
            var mealsParamAndFilter = mealsParamEndpoints.MapGroup("/meals/{mealsId:int}").AddEndpointFilter(new MealIsLockedFilter(5));

            mealsEndpoints.MapGet("", MealsHandlers.GetMealsAsync);
            mealsEndpoints.MapPost("", MealsHandlers.PostMealsAsync);
            mealsParamEndpoints.MapGet("", MealsHandlers.GetMealsByIdAsync).WithName("GetMeals");
            mealsParamAndFilter.MapPut("", MealsHandlers.PutMealsAsync);

            mealsParamAndFilter.MapDelete("", MealsHandlers.DeleteMealsAsync).AddEndpointFilter<LogNotFoundResponseFilter>();

        }

        public static void RegisterIngredientsEndpoints(this IEndpointRouteBuilder endpoints)
        {
            var mealsEndpoints = endpoints.MapGroup("/meals");
            var mealsParamEndpoints = mealsEndpoints.MapGroup("/{mealsId:int}");
            mealsParamEndpoints.MapGet("/ingredients", IngredientsHandlers.GetMealsIngredientsAsync);

            mealsParamEndpoints.MapPost("/ingredients", IngredientsHandlers.GetMealsIngredientsAsync);
        }
    }
}