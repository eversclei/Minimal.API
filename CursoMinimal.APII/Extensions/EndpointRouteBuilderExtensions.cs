using CursoMinimal.API.EndpointHandlers;

namespace CursoMinimal.API.Extensions
{
    public static class EndpointRouteBuilderExtensions
    {
        public static void RegisterMealsEndpoints(this IEndpointRouteBuilder endpoints)
        {
            var mealsEndpoints = endpoints.MapGroup("/meals");
            var mealsParamEndpoints = mealsEndpoints.MapGroup("/{mealsId:int}");

            mealsEndpoints.MapGet("", MealsHandlers.GetMealsAsync);
            mealsEndpoints.MapPost("", MealsHandlers.PostMealsAsync);
            mealsParamEndpoints.MapGet("", MealsHandlers.GetMealsByIdAsync).WithName("GetMeals");
            mealsParamEndpoints.MapPut("", MealsHandlers.PutMealsAsync);
            mealsParamEndpoints.MapDelete("", MealsHandlers.DeleteMealsAsync);
            
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