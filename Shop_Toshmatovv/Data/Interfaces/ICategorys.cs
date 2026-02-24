using Shop_Toshmatovv.Data.Models;

namespace Shop_Toshmatovv.Data.Interfaces
{
    public interface ICategorys
    {
        IEnumerable<Categorys> AllCategories { get; }
    }
}