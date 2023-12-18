using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Dotnet6MvcLogin.Models.Domain
{
    public class Item
    {
        public int Id { get; set; }
        [Required]
        public string? ItemName { get; set; }
        [Required]
        public string? ItemDescription { get; set; }
        public string? ItemSize { get; set; }
        [Required]
        public string? Price { get; set; }
        public string? Quantity { get; set; }

        [Required]
        public string? MadeIn { get; set; }
        public string? ReleaseYear { get; set; }

        public string? ItemImage { get; set; }  // stores movie image name with extension (eg, image0001.jpg)

        [NotMapped]
        public IFormFile? ImageFile { get; set; }
        [NotMapped]
        [Required]
        public List<int>? Categories { get; set; }
        [NotMapped]
        public IEnumerable<SelectListItem>? CategoryList { get; set; }
        [NotMapped]
        public string? CategoryNames { get; set; }

        [NotMapped]
        public MultiSelectList? MultiCategoryList { get; set; }

    }
}
