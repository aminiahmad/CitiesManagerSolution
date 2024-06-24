using CitiesManager.WebAPI.ConfigureServices;

var builder = WebApplication.CreateBuilder(args);
//add services
builder.Services.ConfigurationService(builder.Configuration);
var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseHsts();
app.UseHttpsRedirection();

app.UseSwagger();//create endpoint for swagger.json
app.UseSwaggerUI(option =>
{
    option.SwaggerEndpoint("/swagger/v1/swagger.json","1.0");
    option.SwaggerEndpoint("/swagger/v2/swagger.json","2.0");
});//create swagger ui for testing all web api endpoint / action methods

app.UseAuthorization();

app.MapControllers();

app.Run();
