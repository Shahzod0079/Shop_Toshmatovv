using Microsoft.AspNetCore.Mvc;
using Shop_Toshmatovv.Data.Interfaces;
using Shop_Toshmatovv.Data.ViewModell;
using Shop_Toshmatovv.Data.Models;
using System.Linq;

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

        public ViewResult List(int id = 0, string sortOrder = "asc", string searchString = "")
        {
            ViewBag.Title = "Страница с предметами";
            ViewBag.CurrentSearch = searchString; 

            if (!string.IsNullOrWhiteSpace(searchString))
            {
                VMItems.Items = IAllItems.FindItems(searchString);
            }
            else
            {
                VMItems.Items = IAllItems.AllItems;
            }

            VMItems.Categorys = IAllCategories.AllCategories;
            VMItems.SelectCategory = id;
            VMItems.SortOrder = sortOrder;
            VMItems.SearchString = searchString;

            if (sortOrder == "asc")
            {
                VMItems.Items = VMItems.Items.OrderBy(i => i.Price);
            }
            else
            {
                VMItems.Items = VMItems.Items.OrderByDescending(i => i.Price);
            }

            return View(VMItems);
        }
    }
}