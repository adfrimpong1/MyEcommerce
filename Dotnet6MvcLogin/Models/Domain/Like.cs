using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Dotnet6MvcLogin.Models.Domain
{
    public class Like
    {
        [Key]
        public int Id { get; set; }
        [ForeignKey("Presentation")]
        public int PresentationId { get; set; }
        [ForeignKey("ApplicationUser")]
        public int UserId { get; set; }
        public string Like_date { get; set;}
    }
}
