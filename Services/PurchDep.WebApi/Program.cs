using PurchDep.Dal.Entities;
using PurchDep.Domain.Base;
using PurchDep.Interfaces.Base.Mapping;
using PurchDep.Interfaces.Base.Services;
using PurchDep.Interfaces.Mapping;
using PurchDep.Interfaces.Repositories;
using PurchDep.Interfaces.Services;
using PurchDep.WebApi.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;

// Add services to the container.
builder.Services.AddAppContext(configuration);;
builder.Services.AddScoped<IMappingService<Product, IProduct>, ProductMappingService>();
builder.Services.AddScoped<IMappingService<Supplier, ISupplier>, SupplierMappingService>();
builder.Services.AddScoped<Repository<Product>, ProductRepository>();
builder.Services.AddScoped<Repository<Supplier>, SupplierRepository>();
builder.Services.AddScoped<Service<Product, IProduct>, ProductService>();
builder.Services.AddScoped<Service<Supplier, ISupplier>, SupplierService>();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
