using System;
using Microsoft.AspNet.Mvc.Filters;

namespace MvcBasic.Filters
{
    public class DemoActionResultFilter : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            Console.WriteLine("Action Filter - Before");
        }

        public override void OnActionExecuted(ActionExecutedContext context)
        {
            Console.WriteLine("Action Filter - After");
        }

        public override void OnResultExecuting(ResultExecutingContext context)
        {
            Console.WriteLine("Result Filter - Before");
        }

        public override void OnResultExecuted(ResultExecutedContext context)
        {
            Console.WriteLine("Result Filter - After");
        }
    }
}
