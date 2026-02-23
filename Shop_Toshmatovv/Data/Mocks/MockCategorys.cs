using Shop_Toshmatovv.Data.Interfaces;
using Shop_Toshmatovv.Data.Models;

namespace Shop.Data.Mocks
{
    public class MockCategorys : ICategorys
    {
        public IEnumerable<Categories> AllCategories
        {
            get
            {
                return new List<Categories>
                {
                    new Categories()
                    {
                        Id = 0,
                        Name = "Микроволновые печи",
                        Description = "Микроволновая печь – электроприбор, позволяющий выполнять различные виды работ",
                    },
                    new Categories()
                    {
                        Id = 1,
                        Name = "Мультиварки",
                        Description = "Мультиварка – многофункциональный бытовой прибор для приготовления пищи",
                    }
                };
            }
        }
    }
}
    
