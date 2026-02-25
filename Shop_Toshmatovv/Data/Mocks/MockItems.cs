using Shop_Toshmatovv.Data.Interfaces;
using Shop_Toshmatovv.Data.Models;
using System.Linq;

namespace Shop_Toshmatovv.Data.Mocks
{
    public class MockItems : IItems
    {
        private readonly ICategorys _category;

        public MockItems()
        {
            _category = new MockCategorys();
        }

        public IEnumerable<Items> AllItems
        {
            get
            {
                return new List<Items>
                {
                    new Items()
                    {
                        Id = 1,
                        Name = "DEXP MS-70",
                        Description = "Благодаря черному корпусу с лаконичным дизайном микроволновка DEXP MS-70 станет достойным дополнением интерьера кухни.",
                        Img = "/images/Микроволновка.png",  
                        Price = 3699,
                        Categorys = _category.AllCategories.First(x => x.Id == 1)
                    },
                    new Items()
                    {
                        Id = 2,
                        Name = "Samsung MS23K3513AK",
                        Description = "Микроволновая печь Samsung MS23K3513AK с грилем и керамическим покрытием внутренней камеры.",
                        Img = "/images/Микроволновка 1.png",  
                        Price = 8999,
                        Categorys = _category.AllCategories.First(x => x.Id == 1)
                    },
                    new Items()
                    {
                        Id = 3,
                        Name = "LG MH6565CIS",
                        Description = "Микроволновая печь LG MH6565CIS с грилем и конвекцией.",
                        Img = "/images/Микроволновка 2.png",
                        Price = 12999,
                        Categorys = _category.AllCategories.First(x => x.Id == 1)
                    },
                    new Items()
                    {
                        Id = 4,
                        Name = "BBK 20MWS-722S",
                        Description = "Микроволновая печь BBK 20MWS-722S с механическим управлением.",
                        Img = "/images/Микроволновка 3.png",
                        Price = 2999,
                        Categorys = _category.AllCategories.First(x => x.Id == 2)
                    },
                    new Items()
                    {
                        Id = 5,
                        Name = "Panasonic NN-SD366M",
                        Description = "Микроволновая печь Panasonic NN-SD366M с инверторным управлением.",
                        Img = "/images/Микроволновка 4.png",
                        Price = 10999,
                        Categorys = _category.AllCategories.First(x => x.Id == 2)
                    }
                };
            }
        }

        public int Add(Items item)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Items> FindItems(string searchString)
        {
            throw new NotImplementedException();
        }
        public Items GetItem(int id)
        {
            return AllItems.FirstOrDefault(i => i.Id == id);
        }

        public void Update(Items item)
        {
            // Для Mock просто ничего не делаем или можно обновить в списке
            var existingItem = AllItems.FirstOrDefault(i => i.Id == item.Id);
            if (existingItem != null)
            {
                existingItem.Name = item.Name;
                existingItem.Description = item.Description;
                existingItem.Price = item.Price;
                existingItem.Img = item.Img;
                existingItem.Categorys = item.Categorys;
            }
        }

        public void Delete(int id)
        {
            // Для Mock тоже заглушка
        }
    }
}