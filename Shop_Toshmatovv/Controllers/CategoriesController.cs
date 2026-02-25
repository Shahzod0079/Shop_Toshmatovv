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

        public IActionResult Index()
        {
            var categories = _categories.AllCategories.ToList();
            var allItems = _items.AllItems.ToList();

            foreach (var category in categories)
            {
                ViewData["Count_" + category.Id] = allItems.Count(i => i.Categorys.Id == category.Id);
            }

            return View(categories);
        }

        public IActionResult List(int id)
        {
            var category = _categories.GetCategory(id);
            if (category == null) return NotFound();

            // Получаем все товары и фильтруем по категории
            var allItems = _items.AllItems.ToList();
            var items = allItems.Where(i => i.Categorys?.Id == id).ToList();

            ViewBag.CategoryName = category.Name;
            ViewBag.CategoryId = category.Id;

            return View(items);  // Возвращаем List<Items>
        }

        [HttpGet]
        public IActionResult Add(int? categoryId = null)
        {
            var categories = _categories.AllCategories;

            // Передаем categoryId в ViewBag
            ViewBag.CategoryId = categoryId;  // ← ЭТО ВАЖНО!

            if (categoryId.HasValue)
            {
                ViewBag.SelectedCategoryId = categoryId.Value;
            }

            return View(categories);
        }

        [HttpPost]
        public IActionResult Add(string name, string description)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                ViewBag.Error = "Название категории обязательно";
                return View();
            }

            Categorys newCategory = new Categorys
            {
                Name = name,
                Description = description ?? ""
            };

            _categories.Add(newCategory); 

            return RedirectToAction("Index");
        }
        // ИЗМЕНЕНИЕ
        [HttpGet]
        public IActionResult Edit(int id)
        {
            var category = _categories.GetCategory(id);
            if (category == null) return NotFound();
            return View(category);
        }

        [HttpPost]
        public IActionResult Edit(Categorys category)
        {
            if (string.IsNullOrWhiteSpace(category.Name))
            {
                ViewBag.Error = "Название категории обязательно";
                return View(category);
            }

            _categories.Update(category);
            return RedirectToAction("Index");
        }

        // УДАЛЕНИЕ
        [HttpPost]
        public IActionResult Delete(int id)
        {
            _categories.Delete(id);
            return RedirectToAction("Index");
        }
    }
}