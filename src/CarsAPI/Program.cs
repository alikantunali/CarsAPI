//global using CarsAPI.Data; // no need to add everysingle time with global 
global using Microsoft.EntityFrameworkCore;
using Common.DbDataContext;
using Common.Models;
using Common.Repositories.CarDbListService;
using Common.Repositories.CarListService;
using Microsoft.AspNetCore.Mvc;
using Azure.Identity;

var builder = WebApplication.CreateBuilder(args);

//var keyVaultEndpoint = new Uri(Environment.GetEnvironmentVariable("VaultUri"));

//builder.Configuration.AddAzureKeyVault(keyVaultEndpoint, new DefaultAzureCredential());

// Add services to the container.
//services collection will be configured below. Service is a component. 

builder.Services.AddResponseCaching();
//ADDED XML RETURN OPTION 
builder.Services.AddControllers(options =>
{
    options.ReturnHttpNotAcceptable = true;
    options.CacheProfiles
    .Add("VaryUserAgentHeader_Default30", new CacheProfile(){Duration = 30, VaryByHeader= "User-Agent"});
    
}).AddXmlDataContractSerializerFormatters();




// ADDED SINGLETON LIST OBJECT FOR THE METHODS WORK ON LIST RATHER THAN DB
builder.Services.AddSingleton<CarsDataStore>();


// FOR REPOSITORY PATTERN THE BEST PRACTICE SCOPED
builder.Services.AddScoped<ICarInfoRepository, CarInfoRepository>();

builder.Services.AddScoped<IDbCarInfoRepository,DbCarInfoRepository>();

// ADDED DB CONTEXT TO WORK WITH DATA STORED IN DB.
// CONNECTION STRING IS USED FROM SETINGS.JSON
// LIFETIME IMPLEMENTED FOR DB CONTEXT. IT IS CREATED PER REQUEST


builder.Services.AddDbContext<CarDataContext>(options =>      
                                                                
{

    options.UseSqlServer(builder.Configuration.GetConnectionString("CarsDbSQLConnection"),providerOptions => providerOptions.EnableRetryOnFailure());
    options.UseSqlServer(b => b.MigrationsAssembly("Common"));




});


// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle




builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.

// only runs swagger in development environment.
//MIDDLEWARE SECTION. in the request pipeline  pipeline can include below :DIAGNOSTICS, AUTHENTICATION, ROUTING, ENDPOINT
//if (app.Environment.IsDevelopment())        
    app.UseSwagger();


   // app.UseSwaggerUI();
    app.UseSwaggerUI(c =>
    {
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Cars API v1");
    c.RoutePrefix = String.Empty;
    });

app.UseHttpsRedirection();

app.UseResponseCaching();

app.UseAuthorization();

//app.UseRouting();

//INSTEAD OF ADDING ROUTING AND ENDPOINT MIDDLEWARE WE USE MAPCONTROLLERS, THANKS TO .NET CORE 6*/
app.MapControllers();


app.Run();
