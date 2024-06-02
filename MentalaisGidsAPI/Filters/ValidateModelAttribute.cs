using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

public class ValidateModelAttribute : ActionFilterAttribute
{
    public override void OnActionExecuting(ActionExecutingContext context)
    {
        if (!context.ModelState.IsValid)
        {
            var allErrors = context.ModelState.Values.SelectMany(v => v.Errors);
            context.Result = new BadRequestObjectResult(allErrors);
        }
    }
}
