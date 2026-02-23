using Shop_Toshmatovv.Data.Mocks;
using Shop_Toshmatovv.Data.Interfaces;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();

builder.Services.AddTransient<ICategorys, MockCategorys>();
builder.Services.AddTransient<IItems, Shop_Toshmatovv.Data.Mocks.MockItems>();

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