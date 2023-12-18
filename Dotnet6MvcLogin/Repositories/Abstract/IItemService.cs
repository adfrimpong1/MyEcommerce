using Dotnet6MvcLogin.Models.Domain;
using Dotnet6MvcLogin.Models.DTO;

namespace Dotnet6MvcLogin.Repositories.Abstract
{
    public interface IItemService
    {
       bool Add(Item model);
       bool Update(Item model);
       Item  GetById(int id);
       bool Delete(int id);
       ItemListVm List(string term = "", bool paging = false, int currentPage = 0);
        List<int> GetCategoryByItemId(int itemId);

    }
}
