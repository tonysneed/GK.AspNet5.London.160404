using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNet.Mvc.Rendering;
using MvcBasic.Models;

namespace MvcBasic.ViewModels
{
    public class DelegateViewModel
    {
        readonly List<Course> _courses = new List<Course>
        {
            new Course { Name = "WCF" },
            new Course { Name = "Web API" },
            new Course { Name = "ASP.NET 5" },
        };

        public DelegateViewModel()
        {
            Courses = _courses.Select
                (c => new SelectListItem{Text = c.Name});
        }

        public Person Person { get; set; }

        public string Course { get; set; }
    
        public IEnumerable<SelectListItem> Courses { get; set; }
    }
}
