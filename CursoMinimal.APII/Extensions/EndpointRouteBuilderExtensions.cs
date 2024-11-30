using CursoMinimal.API.EndpointFilters;
using CursoMinimal.API.EndpointHandlers;

namespace CursoMinimal.API.Extensions
{
    public static class EndpointRouteBuilderExtensions
    {
        public static void RegisterMealsEndpoints(this IEndpointRouteBuilder endpoints)
        {

            endpoints.MapGet("/depreciatedexample", (int param) => "Test to deprecate the endpoint").WithOpenApi(o =>
            {
                o.Deprecated = true;
                return o;
            }).WithSummary("This endpoint is deprecated and will be deprecated in V2")
            .WithDescription("Here description for which endpoint to use");

            var mealsEndpoints = endpoints.MapGroup("/meals").RequireAuthorization("RequireAdminFromBrazil").RequireAuthorization();
            var mealsParamEndpoints = mealsEndpoints.MapGroup("/{mealsId:int}");
            var mealsParamAndFilter = endpoints.MapGroup("/meals/{mealsId:int}").RequireAuthorization().AddEndpointFilter(new MealIsLockedFilter(5));

            mealsEndpoints.MapGet("", MealsHandlers.GetMealsAsync);
            mealsEndpoints.MapPost("", MealsHandlers.PostMealsAsync);
            mealsParamEndpoints.MapGet("", MealsHandlers.GetMealsByIdAsync).WithName("GetMeals");
            mealsParamAndFilter.MapPut("", MealsHandlers.PutMealsAsync);

            mealsParamAndFilter.MapDelete("", MealsHandlers.DeleteMealsAsync).AddEndpointFilter<LogNotFoundResponseFilter>();

        }

        public static void RegisterIngredientsEndpoints(this IEndpointRouteBuilder endpoints)
        {
            var mealsEndpoints = endpoints.MapGroup("/meals").RequireAuthorization("RequireAdminFromBrazil").RequireAuthorization();
            var mealsParamEndpoints = mealsEndpoints.MapGroup("/{mealsId:int}");
            mealsParamEndpoints.MapGet("/ingredients", IngredientsHandlers.GetMealsIngredientsAsync);

            mealsParamEndpoints.MapPost("/ingredients", IngredientsHandlers.GetMealsIngredientsAsync);
        }
    }
}