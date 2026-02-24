using Shop_Toshmatovv.Data.Models;

namespace Shop_Toshmatovv.Data.Interfaces
{
    public interface IItems
    {
        IEnumerable<Items> AllItems { get; }
        IEnumerable<Items> FindItems(string searchString);
        int Add(Items item); // Добавить этот метод
    }
}