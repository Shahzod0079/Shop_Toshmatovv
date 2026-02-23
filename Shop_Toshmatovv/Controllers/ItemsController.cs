using Microsoft.AspNetCore.Mvc;
using Shop_Toshmatovv.Data.Interfaces;

namespace Shop_Toshmatovv.Controllers
{
    public class ItemsController : Controller
    {
        private readonly IItems _items;

        public ItemsController(IItems items)
        {
            _items = items;
        }

        public IActionResult List()
        {
            var items = _items.AllItems;
            return View(items);
        }
    }
}