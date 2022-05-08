using adAPI;
using adAPI.Contracts;
using adAPI.Data;
using adAPI.Data.Mappers;
using adAPI.Data.Repositories;
using adAPI.Models;
using adAPI.Services;
using FluentValidation.AspNetCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("adAPI")));

builder.Services.AddScoped<IRepository<Advertisement>, AdvertisementRepository>();
builder.Services.AddScoped<IQueryManipulation, QueryManipulation>();
builder.Services.AddScoped<IAdvertisementService, AdvertisementService>();

builder.Services.AddAutoMapper(typeof(AppMapperProfile));

builder.Services.AddControllers()
    .AddNewtonsoftJson(options =>
    {
        options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
    })
    .AddFluentValidation(fv =>
    {
        fv.ImplicitlyValidateChildProperties = true;
        fv.ImplicitlyValidateRootCollectionElements = true;

        fv.RegisterValidatorsFromAssembly(Assembly.GetExecutingAssembly());
    });

builder.Services.AddEndpointsApiExplorer();


static string GetXmlCommentsPath()
{
    return String.Format(@"{0}\adAPI.XML", AppDomain.CurrentDomain.BaseDirectory);
}

builder.Services.AddSwaggerGen(c =>
{
    c.EnableAnnotations();
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Apdertisement API", Description = "THis API using for works with advertisements." , Version = "v1" });
    c.IncludeXmlComments(GetXmlCommentsPath());
});

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI(x =>
{
    x.SwaggerEndpoint("/swagger/v1/swagger.json", "ad API V1");
    x.RoutePrefix = String.Empty;
});

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
