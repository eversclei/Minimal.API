
namespace CursoMinimal.API.EndpointFilters
{
    public class MealIsLockedFilter(int lockedMealId) : IEndpointFilter
    {
        public readonly int _lockedMealId = lockedMealId;

        public async ValueTask<object?> InvokeAsync(EndpointFilterInvocationContext context, EndpointFilterDelegate next)
        {
            int mealId;
            var requestMethod = context.HttpContext.Request.Method;
            if (requestMethod == "PUT")
            {
                mealId = context.GetArgument<int>(2);
            }
            else if (requestMethod == "DELETE")
            {
                mealId = context.GetArgument<int>(1);
            }
            else
            {
                throw new NotSupportedException("This filter is not supported for this scenario");
            }
            
       
            if (mealId == _lockedMealId)
            {
                return TypedResults.Problem(new()
                {
                    Status = 400,
                    Title = "Fish dish cannot be modified ou deleted",
                    Detail = "You can't take the main fish dish off the list"
                });
            }
            return await next.Invoke(context);
        }
    }
}
