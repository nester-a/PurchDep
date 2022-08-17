using PurchDep.Dal;
using PurchDep.Dal.Entities;
using PurchDep.Interfaces.Base.Mapping;
using PurchDep.Interfaces.Base.Services;
using PurchDep.Interfaces.Mapping;
using PurchDep.Interfaces.Repositories;
using PurchDep.Interfaces.Services;
using PurchDep.WebApi.DependencyInjection;

using ProductDom = PurchDep.Domain.Product;
using SuppliersProductDom = PurchDep.Domain.SuppliersProduct;
using StocksProductDom = PurchDep.Domain.StocksProduct;
using SupplierDom = PurchDep.Domain.Supplier;
using StockDom = PurchDep.Domain.Stock;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;

// Add services to the container.
//вы€влен баг, неподключаетс€ база данных
builder.Services.AddAppContext(configuration);
builder.Services.AddTransient<MappingService<Product, ProductDom>, ProductMappingService>();
builder.Services.AddTransient<MappingService<SuppliersProduct, SuppliersProductDom>, SuppliersProductMappingService>();
builder.Services.AddTransient<MappingService<StocksProduct, StocksProductDom>, StocksProductMappingService>();
builder.Services.AddTransient<MappingService<Supplier, SupplierDom>, SupplierMappingService>();
builder.Services.AddTransient<MappingService<Stock, StockDom>, StockMappingService>();
builder.Services.AddTransient<Repository<Product>, ProductRepository>();
builder.Services.AddTransient<Repository<Supplier>, SupplierRepository>();
builder.Services.AddTransient<Repository<Stock>, StockRepository>();
builder.Services.AddTransient<Service<Product, ProductDom>, ProductService>();
builder.Services.AddTransient<Service<Supplier, SupplierDom>, SupplierService>();
builder.Services.AddTransient<Service<Stock, StockDom>, StockService>();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
}


app.UseSwagger();
app.UseSwaggerUI();
using (var scope = app.Services.CreateScope())
{
    PurchDepContext context = scope.ServiceProvider.GetRequiredService<PurchDepContext>();
    DbInitializer.Initialize(context);
}

app.UseAuthorization();

app.MapControllers();

app.Run();

public partial class Program { }
