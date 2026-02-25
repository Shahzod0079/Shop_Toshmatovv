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
        public ViewResult Add()
        {
            IEnumerable<Categorys> categorys = _iAllCategorys.AllCategories;
            return View(categorys);
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
            return Redirect("/Items/List");
        }

        [HttpGet]
        public IActionResult Update(int id)  
        {
            var item = _iAllItems.GetItem(id);

            if (item == null)
            {
                return Redirect("/Items/List");
            }

            var categories = _iAllCategorys.AllCategories;

            ViewBag.Categories = categories;
            return View(item);
        }
        [HttpPost]
        public RedirectResult Update(int id, string name, string description, IFormFile files, float price, int idCategory)
        {
            var item = _iAllItems.GetItem(id); 

            string fileName = item.Img;

            if (files != null)
            {
                fileName = files.FileName;
                var uploads = Path.Combine(_hostingEnvironment.WebRootPath, "Image");
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
            return Redirect("/Items/List");
        }

        [HttpGet]
        public RedirectResult Delete(int id)
        {
            _iAllItems.Delete(id);  
            return Redirect("/Items/List");
        }
    }
}