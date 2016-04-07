using System;
using Microsoft.AspNet.Mvc.Filters;

namespace MvcBasic.Filters
{
    public class DemoResourceFilter : IResourceFilter
    {
        public void OnResourceExecuting(ResourceExecutingContext context)
        {
            Console.WriteLine("Resource Filter - Before");
        }

        public void OnResourceExecuted(ResourceExecutedContext context)
        {
            Console.WriteLine("Resource Filter - After");
        }
    }
}
