using Shop_Toshmatovv.Data.Models;
using System.Collections.Generic;

namespace Shop_Toshmatovv.Data.ViewModell
{
    public class VMItems
    {
        public IEnumerable<Items> Items { get; set; }
        public IEnumerable<Categorys> Categorys { get; set; }
        public int SelectCategory { get; set; }
        public string SortOrder { get; set; } = "asc";
    }
}