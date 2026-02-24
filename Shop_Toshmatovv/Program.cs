using Shop_Toshmatovv.Data.Interfaces;
using Shop_Toshmatovv.Data.DataBase;  // Добавь эту строку

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();

// ЗАМЕНИ ЭТИ СТРОКИ:
// builder.Services.AddTransient<ICategorys, MockCategorys>();
// builder.Services.AddTransient<IItems, Shop_Toshmatovv.Data.Mocks.MockItems>();

// НА ЭТИ:
builder.Services.AddTransient<ICategorys, DBCategory>();
builder.Services.AddTransient<IItems, DBItems>();

var app = builder.Build();

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