using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Dotnet6MvcLogin.Models.Domain
{
    public class Presentation
    {
        [Key]
        public int Id { get; set; }
        [ForeignKey("ApplicationUser")]
        public int UserId { get; set; }
        public string Title { get; set; }
        public string Upload_date { get; set; }
        public string Visibility { get; set;}
    }
}
