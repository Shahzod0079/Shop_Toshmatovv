using Shop_Toshmatovv.Data.Models;

namespace Shop_Toshmatovv.Data.Interfaces
{
    public interface ICategorys
    {
        IEnumerable<Categorys> AllCategories { get; }
        Categorys GetCategory(int id);             
        void Add(Categorys category);             
        void Update(Categorys category);          
        void Delete(int id);                       
    }
}