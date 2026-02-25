using Shop_Toshmatovv.Data.Interfaces;
using Shop_Toshmatovv.Data.Models;

namespace Shop_Toshmatovv.Data.Mocks
{
    public class MockCategorys : ICategorys
    {
        private List<Categorys> _categories;

        public MockCategorys()
        {
            _categories = new List<Categorys>
            {
                new Categorys()
                {
                    Id = 1,
                    Name = "Микроволновые печи",
                    Description = "Микроволновая печь – электроприбор, позволяющий выполнять различные виды работ",
                    Items = new List<Items>()
                },
                new Categorys()
                {
                    Id = 2,
                    Name = "Мультиварки",
                    Description = "Мультиварка – многофункциональный бытовой прибор для приготовления пищи",
                    Items = new List<Items>()
                }
            };
        }

        public IEnumerable<Categorys> AllCategories
        {
            get
            {
                return _categories;
            }
        }

        public Categorys GetCategory(int id)
        {
            return _categories.FirstOrDefault(c => c.Id == id);
        }

        public void Add(Categorys category)
        {
            category.Id = _categories.Max(c => c.Id) + 1;
            _categories.Add(category);
        }

        public void Update(Categorys category)
        {
            var existing = _categories.FirstOrDefault(c => c.Id == category.Id);
            if (existing != null)
            {
                existing.Name = category.Name;
                existing.Description = category.Description;
            }
        }

        public void Delete(int id)
        {
            var category = _categories.FirstOrDefault(c => c.Id == id);
            if (category != null)
            {
                _categories.Remove(category);
            }
        }
    }
}