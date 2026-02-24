using Microsoft.AspNetCore.Mvc;
using Shop_Toshmatovv.Data.Interfaces;
using Shop_Toshmatovv.Data.ViewModell;
using Shop_Toshmatovv.Data.Models;


namespace Shop_Toshmatovv.Controllers
{
    public class ItemsController : Controller
    {
        private readonly IItems _iAllItems;
        private readonly ICategorys _iAllCategories;
        private readonly IWebHostEnvironment _hostingEnvironment;
        private VMItems _vMItems = new VMItems();

        public ItemsController(IItems itemsRepository, ICategorys categoriesRepository, IWebHostEnvironment environment)
        {
            _iAllItems = itemsRepository;
            _iAllCategories = categoriesRepository;
            _hostingEnvironment = environment;
        }

        public ViewResult List(int id = 0, string sortOrder = "asc", string searchString = "")
        {
            ViewBag.Title = "Страница с предметами";
            ViewBag.CurrentSearch = searchString;

            if (!string.IsNullOrWhiteSpace(searchString))
            {
                _vMItems.Items = _iAllItems.FindItems(searchString);
            }
            else
            {
                _vMItems.Items = _iAllItems.AllItems;
            }

            _vMItems.Categorys = _iAllCategories.AllCategories;
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
            IEnumerable<Categorys> categorys = _iAllCategories.AllCategories;
            return View(categorys);
        }

        /// <summary>
        /// Метод добавления предмета
        /// </summary>
        /// <param name="name">Наименование предмета</param>
        /// <param name="description">Описание предмета</param>
        /// <param name="files">Изображение</param>
        /// <param name="price">Цена</param>
        /// <param name="idCategory">Код категории</param>
        /// <returns></returns>
        [HttpPost]
        public RedirectResult Add(string name, string description, IFormFile files, float price, int idCategory)
        {
            string fileName = "";

            if (files != null)
            {
                fileName = files.FileName;
                var uploads = Path.Combine(_hostingEnvironment.WebRootPath, "img");
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

            int id = _iAllItems.Add(newItems);
            return Redirect("/Items/Update?id=" + id);
        }
    }
}