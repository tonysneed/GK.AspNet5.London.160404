using System.ComponentModel.DataAnnotations;

namespace Authentication.Models
{
    public class CustomersViewModel
    {
        [Required]
        public string CustomerName { get; set; }

        [Required]
        public string Location { get; set; }
    }
}
