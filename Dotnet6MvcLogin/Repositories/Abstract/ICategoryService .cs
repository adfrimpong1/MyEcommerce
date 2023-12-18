using Dotnet6MvcLogin.Models.DTO;
using Dotnet6MvcLogin.Models.Domain;

namespace Dotnet6MvcLogin.Repositories.Abstract
{
    public interface ICategoryService
    {

        bool Add(Category model);
        bool Update(Category model);
        Category GetById(int id);
        bool Delete(int id);
        IQueryable<Category> List();
    }
}
