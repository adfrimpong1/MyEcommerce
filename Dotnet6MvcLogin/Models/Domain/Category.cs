using System.ComponentModel.DataAnnotations;

namespace Dotnet6MvcLogin.Models.Domain
{
    public class Category
    {
        public int Id { get; set; }

        [Required]
        public string? CategoryName { get; set; }

    }
}
