using Shop.Data.Mocks;
using Shop_Toshmatovv.Data.Interfaces;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddMvc(option => option.EnableEndpointRouting = false);
builder.Services.AddTransient<ICategorys, MockCategorys>();
builder.Services.AddTransient<IItems, MockItems>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}

app.UseStatusCodePages();
app.UseStaticFiles();
app.UseMvcWithDefaultRoute();
app.Run();