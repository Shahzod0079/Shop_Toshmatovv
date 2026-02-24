using Microsoft.AspNetCore.Mvc;
using Shop_Toshmatovv.Data.Interfaces;
using Shop_Toshmatovv.Data.ViewModell;
using Shop_Toshmatovv.Data.Models;

namespace Shop_Toshmatovv.Controllers
{
    public class ItemsController : Controller
    {
        private IItems IAllItems;

        private ICategorys IAllCategories;

        VMItems VMItems = new VMItems();

        public ItemsController(IItems IAllItems, ICategorys IAllCategories)
        {
            this.IAllItems = IAllItems;

            this.IAllCategories = IAllCategories;
        }

        public ViewResult List(int id = 0)
        {
            ViewBag.Title = "Страница с предметами";



            VMItems.Items = IAllItems.AllItems;

            VMItems.Categorys = IAllCategories.AllCategories;

            VMItems.SelectCategory = id;

            return View(VMItems);
        }
    }
}