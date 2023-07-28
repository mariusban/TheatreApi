using TheatreApi.Api.Converter;
using TheatreApi.DataAccess.Repositories;
using TheatreApi.DataAccess.Context;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddScoped<ITheatreRepository, TheatreRepository>();
builder.Services.AddScoped<IActorConverter, ActorConverter>();
builder.Services.AddScoped<IPlayConverter, PlayConverter>();
builder.Services.AddScoped<ITheatreConverter, TheatreConverter>();

builder.Services.AddDbContext<TheatreDBContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("TheatreDB")));

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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
