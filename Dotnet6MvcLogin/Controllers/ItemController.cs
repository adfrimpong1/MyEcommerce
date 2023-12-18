using Humanizer.Localisation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Dotnet6MvcLogin.Models.Domain;
using Dotnet6MvcLogin.Repositories.Abstract;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Dotnet6MvcLogin.Controllers
{
    public class ItemController : Controller
    {
        private readonly IItemService _itemService;
        private readonly IFileService _fileService;
        private readonly ICategoryService _catService;
        public ItemController(ICategoryService catService, IItemService ItemService, IFileService fileService)
        {
            _itemService = ItemService;
            _fileService = fileService;
            _catService = catService;
        }
        public IActionResult Add()
        {
            var model = new Item();
            model.CategoryList = _catService.List().Select(a => new SelectListItem { Text = a.CategoryName, Value = a.Id.ToString() });
            return View(model);
        }

        [HttpPost]
        public IActionResult Add(Item model)
        {
            model.CategoryList = _catService.List().Select(a => new SelectListItem { Text = a.CategoryName, Value = a.Id.ToString() });
            if (!ModelState.IsValid)
                return View(model);
            if (model.ImageFile != null)
            {
                var fileReult = this._fileService.SaveImage(model.ImageFile);
                if (fileReult.Item1 == 0)
                {
                    TempData["msg"] = "File could not saved";
                    return View(model);
                }
                var imageName = fileReult.Item2;
                model.ItemImage = imageName;
            }
            var result = _itemService.Add(model);
            if (result)
            {
                TempData["msg"] = "Added Successfully";
                return RedirectToAction(nameof(Add));
            }
            else
            {
                TempData["msg"] = "Error on server side";
                return View(model);
            }
        }

        public IActionResult Edit(int id)
        {
            var model = _itemService.GetById(id);
            var selectedCategorys = _itemService.GetCategoryByItemId(model.Id);
            MultiSelectList multiCategoryList = new MultiSelectList(_catService.List(), "Id", "CategoryName", selectedCategorys);
            model.MultiCategoryList = multiCategoryList;
            return View(model);
        }

        [HttpPost]
        public IActionResult Edit(Item model)
        {
            var selectedCategorys = _itemService.GetCategoryByItemId(model.Id);
            MultiSelectList multiCategoryList = new MultiSelectList(_catService.List(), "Id", "CategoryName", selectedCategorys);
            model.MultiCategoryList = multiCategoryList;
            if (!ModelState.IsValid)
                return View(model);
            if (model.ImageFile != null)
            {
                var fileReult = this._fileService.SaveImage(model.ImageFile);
                if (fileReult.Item1 == 0)
                {
                    TempData["msg"] = "File could not save";
                    return View(model);
                }
                var imageName = fileReult.Item2;
                model.ItemImage = imageName;
            }
            var result = _itemService.Update(model);
            if (result)
            {
                TempData["msg"] = "Added Successfully";
                return RedirectToAction(nameof(ItemList));
            }
            else
            {
                TempData["msg"] = "Error on server side";
                return View(model);
            }
        }

       

        public IActionResult ItemList()
        {
            var data = this._itemService.List();
            return View(data);
        }

        public IActionResult Delete(int id)
        {
            var result = _itemService.Delete(id);
            return RedirectToAction(nameof(ItemList));
        }

        [Route("/ApiItems")]
        public IActionResult ApiItems()
        {
            var products = _itemService.List();
            
            return Ok(products);
        }
    }
    
}
