using Dotnet6MvcLogin.Models.Domain;
using Dotnet6MvcLogin.Models.DTO;
using Dotnet6MvcLogin.Repositories.Abstract;

namespace Dotnet6MvcLogin.Repositories.Implementation
{
    public class ItemService : IItemService
    {
        private readonly DatabaseContext ctx;
        public ItemService(DatabaseContext ctx) 
        {
            this.ctx = ctx;
        }

        public bool Add(Item model)
        {
            try
            {

                ctx.Item.Add(model);
                ctx.SaveChanges();
                foreach (int categoryId in model.Categories)
                {
                    var itemCategory = new ItemCategory
                    {
                        ItemId = model.Id,
                        CategoryId = categoryId
                    };
                    ctx.ItemCategory.Add(itemCategory);
                }
                ctx.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }


        public bool Delete(int id)
        {
            try
            {
                var data = this.GetById(id);
                if (data == null)
                    return false;
                var itemCategories = ctx.ItemCategory.Where(a => a.ItemId == data.Id);
                foreach (var itemCategory in itemCategories)
                {
                    ctx.ItemCategory.Remove(itemCategory);
                }
                ctx.Item.Remove(data);
                ctx.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public Item GetById(int id)
        {
            
            var item = ctx.Item.Find(id);
            var obj = ctx.ItemCategory.FirstOrDefault(x => x.ItemId == item.Id);
            var obj1 = ctx.Category.FirstOrDefault(x => x.Id == obj.CategoryId);
            item.CategoryNames = obj1.CategoryName;

            return item;

        }

        public ItemListVm List(string term = "", bool paging = false, int currentPage = 0)
        {
            var data = new ItemListVm();

            var list = ctx.Item.ToList();


            if (!string.IsNullOrEmpty(term))
            {
                term = term.ToLower();
                var temp = list.Where(a => a.ItemName.ToLower().StartsWith(term)).ToList();
                if(temp.Count == 0)
                {
                    temp = list.Where(a => a.Price!=null ? a.Price.ToLower().StartsWith(term): false).ToList();
                }
                list = temp;
            }

            if (paging)
            {
                // here we  will apply paging
                int pageSize = 5;
                int count = list.Count;
                int TotalPages = (int)Math.Ceiling(count / (double)pageSize);
                list = list.Skip((currentPage - 1) * pageSize).Take(pageSize).ToList();
                data.PageSize = pageSize;
                data.CurrentPage = currentPage;
                data.TotalPages = TotalPages;
            }

            foreach (var item in list)
            {
                var categories = (from category in ctx.Category
                                  join mg in ctx.ItemCategory
                                  on category.Id equals mg.CategoryId
                                  where mg.ItemId == item.Id
                                  select category.CategoryName
                              ).ToList();
                var categoryNames = string.Join(',', categories);
                item.CategoryNames = categoryNames;
            }
            data.ItemList = list.AsQueryable();
            return data;
        }

        public bool Update(Item model)
        {
            try
            {
                // these categoryIds are not selected by users and still present is itemCategory table corresponding to
                // this itemId. So these ids should be removed.
                var categoriesToDeleted = ctx.ItemCategory.Where(a => a.ItemId == model.Id && !model.Categories.Contains(a.CategoryId)).ToList();
                foreach (var mCategory in categoriesToDeleted)
                {
                    ctx.ItemCategory.Remove(mCategory);
                }
                foreach (int catId in model.Categories)
                {
                    var itemCategory = ctx.ItemCategory.FirstOrDefault(a => a.ItemId == model.Id && a.CategoryId == catId);
                    if (itemCategory == null)
                    {
                        itemCategory = new ItemCategory { CategoryId = catId, ItemId = model.Id };
                        ctx.ItemCategory.Add(itemCategory);
                    }
                }

                ctx.Item.Update(model);
                // we have to add these category ids in itemCategory table
                ctx.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public List<int> GetCategoryByItemId(int categoryId)
        {
            var categoryIds = ctx.ItemCategory.Where(a => a.ItemId == categoryId).Select(a => a.CategoryId).ToList();
            return categoryIds;
        }

    }
}



