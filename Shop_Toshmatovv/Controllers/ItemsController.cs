using Shop.Data.Mocks;
using Shop_Toshmatovv.Data.Interfaces;

namespace Shop_Toshmatovv.Controllers
{
    public class ItemsController
    {
        public void ConfigureServices(IServiceCollection services)
        {
            // объединяем интерфейс и реализующий класс
            services.AddTransient<ICategorys, MockCategorys>();
            services.AddTransient<IItems, MockItems>();

            // включаем поддержку MVC
            services.AddMvc(option => option.EnableEndpointRouting = false);
        }
    }
}
