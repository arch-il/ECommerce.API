using ECommerce.API.Database.Context;
using ECommerce.API.Interfaces.Order;
using ECommerce.API.Interfaces.Product;
using ECommerce.API.Services.Order;
using ECommerce.API.Services.Product;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<ECommerceContext>(opt => opt.UseSqlServer(builder.Configuration.GetConnectionString("ECommerceDB")));

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddTransient<IProductService, ProductService>();
builder.Services.AddTransient<IOrderService, OrderService>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
