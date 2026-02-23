using Shop_Toshmatovv.Data.Mocks;
using Shop_Toshmatovv.Data.Interfaces;
using Shop_Toshmatovv.Data.Mocks;

var builder = WebApplication.CreateBuilder(args);

// Добавляем MVC
builder.Services.AddControllersWithViews();

// Регистрируем зависимости
builder.Services.AddTransient<ICategorys, MockCategorys>();
builder.Services.AddTransient<IItems, Shop_Toshmatovv.Data.Mocks.MockItems>();

var app = builder.Build();

// Настройка конвейера обработки запросов
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Items}/{action=List}/{id?}");

app.Run();