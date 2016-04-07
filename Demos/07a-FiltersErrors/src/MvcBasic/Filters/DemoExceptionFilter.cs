using System;
using System.Threading.Tasks;
using Microsoft.AspNet.Mvc.Filters;
using Microsoft.Extensions.Logging;
using MvcBasic.Repositories;

namespace MvcBasic.Filters
{
    public class DemoExceptionFilter : ExceptionFilterAttribute
    {
        private readonly ISampleRepository _sampleRepository;
        private readonly ILogger<DemoExceptionFilter> _logger;

        public DemoExceptionFilter(ISampleRepository sampleRepository,
            ILogger<DemoExceptionFilter> logger)
        {
            _sampleRepository = sampleRepository;
            _logger = logger;
        }

        public override async Task OnExceptionAsync(ExceptionContext context)
        {
            var user = await _sampleRepository.GetUserDetailsAsync("Donald");

            string errorMessage = ($"Logging Error from {user}: {context.Exception.Message}");
            //Console.WriteLine(errorMessage);

            // Use the logger
            _logger.LogError(errorMessage);
        }
    }
}
