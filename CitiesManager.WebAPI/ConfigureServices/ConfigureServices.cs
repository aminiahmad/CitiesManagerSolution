using CitiesManager.WebAPI.DatabaseContext;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.EntityFrameworkCore;
namespace CitiesManager.WebAPI.ConfigureServices;

/// <summary>
/// extension method for IServiceCollection for clean code on program.cs
/// </summary>
public static class ConfigureServices
{

    /// <summary>
    /// add services to serviceCollection
    /// </summary>
    /// <param name="services">Object of ServiceCollection to add services to program</param>
    /// <param name="configuration">Configuration for config connection string to sql</param>
    /// <returns>ServiceCollection to builder.services.Name Of Extension Method</returns>
    public static IServiceCollection ConfigurationService(this IServiceCollection services,IConfiguration configuration)
    {
        // Add services to the container.

        services.AddControllers(option =>
        {
            // response body just json format for global controller and method
            option.Filters.Add(new ProducesAttribute("application/json"));
            // request body just json format for global controller and method
            option.Filters.Add(new ConsumesAttribute("application/json"));
        });

        //read version on controllers for .net core
        services.AddApiVersioning(config =>
        {
            config.ApiVersionReader = new UrlSegmentApiVersionReader();// Read version number from request url at 'apiVersion' constraint 
            // default version number is 1.0
            config.DefaultApiVersion = new ApiVersion(1, 0);
            config.AssumeDefaultVersionWhenUnspecified=true;
        });

        services.AddDbContext<ApplicationDbContext>(option =>
        {
            option.UseSqlServer(configuration["ConnectionStrings:default"]);//connect to db
        });

        //enable swagger 
        services.AddEndpointsApiExplorer();//generates description for all endpoints
        services.AddSwaggerGen(option =>
        {
            option.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory,"api.xml"));
            option.SwaggerDoc("v1",new Microsoft.OpenApi.Models.OpenApiInfo(){Title="Cities Web Api",Version = "1.0"});
            option.SwaggerDoc("v2",new Microsoft.OpenApi.Models.OpenApiInfo(){Title="Cities Web Api",Version = "2.0"});
        });//generates openApi specification

        //add version format to swaggerUi in program 
        services.AddVersionedApiExplorer(option =>
        {
            option.GroupNameFormat = "'v'VVV"; //v1
            option.SubstituteApiVersionInUrl=true;
        });

        return services;
    }
}