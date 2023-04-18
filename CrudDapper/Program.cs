using Microsoft.JSInterop.Infrastructure;

using CrudDapper.Repository;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddScoped<IProductRepository, ProductRepository>(); 
/*Inje��o de dependencia, sempre que uma classe ou servi�o precisar de uma instancia da interface 
 * ela ser� buscada na instancia da classe que a interface foi implementada, 
 * ao final da solicita��o a instancia � descartada
 */

builder.Services.AddEndpointsApiExplorer();
IServiceCollection serviceCollection = builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
