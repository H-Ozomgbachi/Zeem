#region Application developed in December 2024 with .NET 8 by Henry Ozomgbachi
WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

//Logger Setup
Log.Logger = new LoggerConfiguration()
    .ReadFrom.Configuration(builder.Configuration)
    .CreateLogger();

// Add services to the container
builder.Services.AddPresentationServices(builder.Configuration);
builder.Services.AddApplicationServices(builder.Configuration);
builder.Services.AddInfrastructureServices(builder.Configuration);

builder.WebHost.ConfigureKestrel(options =>
{
    options.Limits.MaxResponseBufferSize = int.MaxValue;
});

builder.Host.UseSerilog();

WebApplication app = builder.Build();

// Request pipelines registry
app.UseSwagger();
app.UseSwaggerUI(s =>
{
    s.SwaggerEndpoint(builder.Configuration["AppSettings:SwaggerEndpoint"], string.Empty);
    s.DocExpansion(Swashbuckle.AspNetCore.SwaggerUI.DocExpansion.None);
});

app.UseStaticFiles();
app.UseHttpsRedirection();
app.UseCors("CorsPolicy");

app.UseMiddleware<GlobalExceptionMiddleware>();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
#endregion
/*
 * The above code snippet is the entry point of the application. It is the Program.cs file in the ProductService.API project for ZEEM Assessment.
 * * The application is built using the .NET 8 framework and is developed by Henry Ozomgbachi in December 2024.
 * * The application uses Serilog for logging and Swagger for API documentation.
 * * It also includes middleware for handling global exceptions.
 * * The application has separate services for presentation(API), application, infrastructure and domain layers.
 * * The architecture follows the CQRS pattern with MediatR for command and query handling.
 * * UnitOfWork and Repository patterns are used for data access.
 * */