using Microsoft.AspNetCore.Mvc;
using Shop_Toshmatovv.Data.Interfaces;
using Shop_Toshmatovv.Data.ViewModell;
using Shop_Toshmatovv.Data.Models;
using System.Linq;

namespace Shop_Toshmatovv.Controllers
{
    public class ItemsController : Controller
    {
        private readonly IItems _iAllItems;
        private readonly ICategorys _iAllCategorys;
        private readonly IWebHostEnvironment _hostingEnvironment;

        public ItemsController(IItems itemsRepository, ICategorys categoriesRepository, IWebHostEnvironment environment)
        {
            _iAllItems = itemsRepository;
            _iAllCategorys = categoriesRepository;
            _hostingEnvironment = environment;
        }


        [HttpGet]
        public ViewResult List(int id = 0, string sortOrder = "asc", string searchString = "")
        {


            ViewBag.Title = "Страница с предметами";
            ViewBag.CurrentSearch = searchString;

            VMItems _vMItems = new VMItems();

            if (!string.IsNullOrWhiteSpace(searchString))
            {
                _vMItems.Items = _iAllItems.FindItems(searchString);
            }
            else
            {
                _vMItems.Items = _iAllItems.AllItems;
            }

            _vMItems.Categorys = _iAllCategorys.AllCategories;
            _vMItems.SelectCategory = id;
            _vMItems.SortOrder = sortOrder;
            _vMItems.SearchString = searchString;

            if (sortOrder == "asc")
            {
                _vMItems.Items = _vMItems.Items.OrderBy(i => i.Price);
            }
            else
            {
                _vMItems.Items = _vMItems.Items.OrderByDescending(i => i.Price);
            }

            return View(_vMItems);
        }
        [HttpGet]
        public IActionResult Add(int? categoryId = null)
        {
            IEnumerable<Categorys> categories = _iAllCategorys.AllCategories;

            ViewBag.CategoryId = categoryId;

            return View(categories);
        }

        [HttpPost]
        public IActionResult Add(string name, string description, IFormFile files, float price, int idCategory)
        {
            string fileName = "";

            if (files != null)
            {
                fileName = files.FileName;
                var uploads = Path.Combine(_hostingEnvironment.WebRootPath, "Images");

                if (!Directory.Exists(uploads))
                {
                    Directory.CreateDirectory(uploads);
                }

                var filePath = Path.Combine(uploads, fileName);

                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    files.CopyTo(fileStream);
                }
            }

            Items newItems = new Items
            {
                Name = name,
                Description = description,
                Img = fileName,
                Price = Convert.ToInt32(price),
                Categorys = new Categorys() { Id = idCategory }
            };

            _iAllItems.Add(newItems);

            if (Request.Query.ContainsKey("categoryId"))
            {
                int categoryId = int.Parse(Request.Query["categoryId"]);
                return Redirect($"/Categories/List?id={categoryId}");
            }

            return Redirect($"/Categories/List?id={idCategory}");
        }

        [HttpGet]
        public IActionResult Update(int id)
        {
            var item = _iAllItems.GetItem(id);
            if (item == null) return Redirect("/Items/List");

            var categories = _iAllCategorys.AllCategories;

            if (Request.Query.ContainsKey("categoryId"))
            {
                ViewBag.CategoryId = int.Parse(Request.Query["categoryId"]);
            }

            ViewBag.Categories = categories;
            return View(item);
        }
        [HttpPost]
        public IActionResult Update(int id, string name, string description, IFormFile files, float price, int idCategory)
        {
            var item = _iAllItems.GetItem(id);

            string fileName = item.Img;

            if (files != null)
            {
                fileName = files.FileName;
                var uploads = Path.Combine(_hostingEnvironment.WebRootPath, "Images");
                var filePath = Path.Combine(uploads, fileName);

                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    files.CopyTo(fileStream);
                }
            }

            var updatedItem = new Items
            {
                Id = id,
                Name = name,
                Description = description,
                Img = fileName,
                Price = Convert.ToInt32(price),
                Categorys = new Categorys { Id = idCategory }
            };

            _iAllItems.Update(updatedItem);

            if (Request.Query.ContainsKey("categoryId"))
            {
                int categoryId = int.Parse(Request.Query["categoryId"]);
                return Redirect($"/Categories/List?id={categoryId}");
            }

            return Redirect("/Items/List");
        }
        [HttpGet]
        public IActionResult Delete(int id)
        {
            _iAllItems.Delete(id);

            if (Request.Query.ContainsKey("categoryId"))
            {
                int categoryId = int.Parse(Request.Query["categoryId"]);
                return Redirect($"/Categories/List?id={categoryId}");
            }

            return Redirect("/Items/List");
        }


        public ActionResult Basket(int idItem = -1)
        {
            if (idItem != -1)
            {
                var item = _iAllItems.AllItems.FirstOrDefault(x => x.Id == idItem);
                if (item != null)
                {
                    Startup.BasketItem.Add(new ItemsBasket(1, item));
                }
            }
            return Json(Startup.BasketItem);
        }
        public ActionResult BasketCount(int idItem = -1, int count = -1)
        {
            if (idItem != -1)
            {
                var basketItem = Startup.BasketItem.FirstOrDefault(x => x.Item.Id == idItem);
                if (basketItem != null)
                {
                    if (count == 0)
                        Startup.BasketItem.Remove(basketItem);
                    else
                        basketItem.Count = count;
                }
            }
            return Json(Startup.BasketItem);
        }
        public ActionResult GetBasketCount()
        {
            int count = Startup.BasketItem?.Sum(x => x.Count) ?? 0;
            return Json(count);
        }
        public ActionResult BasketPage()
        {
            var basketItems = Startup.BasketItem ?? new List<ItemsBasket>();
            return View(basketItems);
        }

        public ActionResult RemoveFromBasket(int id)
        {
            var item = Startup.BasketItem?.FirstOrDefault(x => x.Item.Id == id);
            if (item != null)
            {
                Startup.BasketItem.Remove(item);
            }
            return RedirectToAction("BasketPage");
        }
    }
}