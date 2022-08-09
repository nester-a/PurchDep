using PurchDep.Domain;
using PurchDep.Interfaces.Base.Services;
using PurchDep.WebApi.Clients.Products;
using PurchDep.WebApi.Clients.Suppliers;

var builder = WebApplication.CreateBuilder(args);
var services = builder.Services;
var configuration = builder.Configuration;

// Add services to the container.
services.AddRazorPages();
services.AddHttpClient("PurchDepWebApi", client => client.BaseAddress = new(configuration["WebApi"]))
    .AddTypedClient<IService<Product>, ProductsClient>()
    .AddTypedClient<IService<Supplier>, SuppliersClient>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
}
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();

app.Run();
