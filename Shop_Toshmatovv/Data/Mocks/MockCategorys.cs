using Shop_Toshmatovv.Data.Interfaces;
using Shop_Toshmatovv.Data.Models;

namespace Shop_Toshmatovv.Data.Mocks
{
    public class MockCategorys : ICategorys
    {
        public IEnumerable<Categorys> AllCategories
        {
            get
            {
                return new List<Categorys>
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
        }
    }
}