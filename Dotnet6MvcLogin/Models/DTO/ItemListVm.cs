using Dotnet6MvcLogin.Models.Domain;

namespace Dotnet6MvcLogin.Models.DTO
{
    public class ItemListVm
    {
        public IQueryable<Item> ItemList { get; set; }
        public int PageSize { get; set; }
        public int CurrentPage { get; set; }
        public int TotalPages { get; set; }
        public string? Term { get; set; }
    }
}
