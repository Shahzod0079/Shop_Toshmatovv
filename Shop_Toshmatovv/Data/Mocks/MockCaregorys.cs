using Shop_Toshmatovv.Data.Interfaces;
using Shop_Toshmatovv.Data.Models;

namespace Shop.Data.Mocks
{
    public class MockItems : IItems
    {
        /// <summary> Интерфейс категорий </summary>
        public ICategorys _category = new MockCategorys();

        /// <summary> Имитируем хранимые данные, через реализацию IEnumerable </summary>
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
                        Description = "Благодаря черному корпусу с лаконичным дизайном микроволновка DEXP MS-70 станет достойным дополнением интерьера кухни. Она имеет вместительность 17 л и мощность 700 Вт, позволяя оперативно разогревать готовые блюда. Механическое управление представлено поворотными переключателями. Таймер рассчитан на 30 минут. Рабочая камера оснащена подсветкой, чтобы вы могли наблюдать за процессом приготовления. Дверца микроволновой печи открывается нажатием на ручку.",
                        Img = "https://c.dns-shop.ru/thumb/54/fit/wm/0/0/d3136d0800646b0ba38a3",
                        Price = 3699,
                        Categorys = _category.AllCategories.First(x => x.Id == 1)
                    },
                    new Items()
                    {
                        Id = 2,
                        Name = "Samsung MS23K3513AK",
                        Description = "Микроволновая печь Samsung MS23K3513AK с грилем и керамическим покрытием внутренней камеры. Объем 23 л, мощность 800 Вт. Режимы: разморозка, разогрев, гриль. 6 уровней мощности. Электронное управление с дисплеем.",
                        Img = "https://c.dns-shop.ru/thumb/54/fit/wm/0/0/7c5d6a4b3c8e4b8a9f1d2e3f4a5b6c7d",
                        Price = 8999,
                        Categorys = _category.AllCategories.First(x => x.Id == 1)
                    },
                    new Items()
                    {
                        Id = 3,
                        Name = "LG MH6565CIS",
                        Description = "Микроволновая печь LG MH6565CIS с грилем и конвекцией. Объем 25 л, мощность 900 Вт. Технология Smart Inverter для равномерного приготовления. Режимы: разморозка, разогрев, гриль, конвекция. Сенсорное управление.",
                        Img = "https://c.dns-shop.ru/thumb/54/fit/wm/0/0/a1b2c3d4e5f6g7h8i9j0k1l2m3n4o5p6",
                        Price = 12999,
                        Categorys = _category.AllCategories.First(x => x.Id == 1)
                    },
                    new Items()
                    {
                        Id = 4,
                        Name = "BBK 20MWS-722S",
                        Description = "Микроволновая печь BBK 20MWS-722S с механическим управлением. Объем 20 л, мощность 700 Вт. Режимы: разморозка, разогрев. Простое и надежное устройство для ежедневного использования.",
                        Img = "https://c.dns-shop.ru/thumb/54/fit/wm/0/0/q1w2e3r4t5y6u7i8o9p0a1s2d3f4g5h6",
                        Price = 2999,
                        Categorys = _category.AllCategories.First(x => x.Id == 1)
                    },
                    new Items()
                    {
                        Id = 5,
                        Name = "Panasonic NN-SD366M",
                        Description = "Микроволновая печь Panasonic NN-SD366M с инверторным управлением. Объем 23 л, мощность 800 Вт. 12 автоматических программ приготовления. Сенсорная панель управления с дисплеем.",
                        Img = "https://c.dns-shop.ru/thumb/54/fit/wm/0/0/z1x2c3v4b5n6m7a8s9d0f1g2h3j4k5l6",
                        Price = 10999,
                        Categorys = _category.AllCategories.First(x => x.Id == 1)
                    }
                };
            }
        }

        public IEnumerable<Items> ALlItems => throw new NotImplementedException();
    }
}
