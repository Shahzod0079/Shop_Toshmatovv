using Shop_Toshmatovv.Data.Models;
using System.Collections.Generic;

namespace Shop_Toshmatovv.Data.ViewModell
{
    public class VMItems
    {
        /// <summary> Предметы </summary>
        public IEnumerable<Items> Items { get; set; }

        /// <summary> Категории </summary>
        public IEnumerable<Categorys> Categorys { get; set; }

        /// <summary> Выбранная категория </summary>
        public int SelectCategory = 0;
    }
}