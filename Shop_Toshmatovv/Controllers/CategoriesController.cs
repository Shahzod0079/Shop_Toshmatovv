using Microsoft.AspNetCore.Mvc;
using Shop_Toshmatovv.Data.Interfaces;
using Shop_Toshmatovv.Data.Models;

namespace Shop_Toshmatovv.Controllers
{
    public class CategoriesController : Controller
    {
        private readonly ICategorys _categories;
        private readonly IItems _items;

        public CategoriesController(ICategorys categories, IItems items)
        {
            _categories = categories;
            _items = items;
        }

        // Отображение всех категорий
        public IActionResult Index()
        {
            var categories = _categories.AllCategories;
            return View(categories);
        }

        // Отображение товаров в конкретной категории
        public IActionResult List(int id)
        {
            var category = _categories.AllCategories.FirstOrDefault(c => c.Id == id);
            if (category == null)
            {
                return NotFound();
            }

            var items = _items.AllItems.Where(i => i.Categorys.Id == id);

            ViewBag.CategoryName = category.Name;
            return View(items);
        }
    }
}