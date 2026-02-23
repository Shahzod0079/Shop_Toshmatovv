using Shop.Data.Mocks;
using Shop_Toshmatovv.Data.Interfaces;
using Shop_Toshmatovv.Data.Mocks;
using Shop_Toshmatovv.Data.Models;

namespace Shop_Toshmatovv
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            // объединяем интерфейс и реализующий класс
            services.AddTransient<ICategorys, MockCategorys>();
            services.AddTransient<IItems, Shop.Data.Mocks.MockItems>();

            // включаем поддержку MVC
            services.AddMvc(option => option.EnableEndpointRouting = false);
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseStatusCodePages();
            app.UseStaticFiles();
            app.UseMvcWithDefaultRoute();
        }
    }
}