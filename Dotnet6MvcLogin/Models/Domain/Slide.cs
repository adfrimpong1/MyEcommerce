using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Dotnet6MvcLogin.Models.Domain
{
    public class Slide
    {
        [Key]
        public int Id { get; set; }
        [ForeignKey("Presentation")]
        public int PresentationId { get; set; }
        [ForeignKey("ApplicationUser")]
        public int UserId { get; set; }
        public int Slide_number { get; set; }
        public string Image_url { get; set; }
    }
}
