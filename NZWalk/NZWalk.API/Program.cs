using Microsoft.EntityFrameworkCore;
using NZWalk.API.Database;
using NZWalk.API.Repositories.WalkRepository;
using NZWalk.API.Repositories.WalkDifficultyRepository;
using NZWalk.API.Repositories.RegionRepository;

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
//as many places we are passing nzWalksdbcontext into ctor parameters: so create a dependency injection that when it is required, .net will create it and also will return db connection string
//dependency injected dbcontextclass into services collection : practice for Entity Framework 
//when I as for NZwalksdbcontext, give me the connection string

builder.Services.AddScoped<IRegionRepository, RegionRepository>();
//as we are passing Iregiorepository in ctor, when it is required, it will create and return regionrepository object : concept of dependency injection
//when I Ask for Iregionrepository, give me implementation of regionRepository
//dependency injection

builder.Services.AddScoped<IWalkRepository, WalkRepository>();
builder.Services.AddScoped<IWalkDifficultyRepository, WalkDifficultyRepository>();

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