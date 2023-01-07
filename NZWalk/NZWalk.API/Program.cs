using Microsoft.EntityFrameworkCore;
using NZWalk.API.Database;
using NZWalk.API.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(); //swagger dependency //disabled from launchsetting.json
builder.Services.AddDbContext<NZWalksDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("NZWalksConnectionName"));

});
//dependency injected dbcontextclass into services collection : practice for Entity Framework 
//when I as for NZwalksdbcontext, give me the connection string

builder.Services.AddScoped<IRegionRepository, RegionRepository>();
//when I Ask for Iregionrepository, give me implementation of regionRepository
//dependency injection

builder.Services.AddAutoMapper(typeof(Program).Assembly);
//dependency injection for automapper



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


//read more about dependency injection