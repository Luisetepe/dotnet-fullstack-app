using FastEndpoints;
using FastEndpoints.Security;
using FastEndpoints.Swagger;
using WebApp.Template.Application;

var builder = WebApplication.CreateBuilder(args);

builder
    .Services.AddFastEndpoints()
    .AddAuthenticationCookie(validFor: TimeSpan.FromMinutes(10))
    .AddAuthorization()
    .SwaggerDocument(opt =>
    {
        opt.DocumentSettings = s =>
        {
            s.Title = "WebApp.Template.Api";
            s.Description = "Example API for a fullstack web application";
            s.Version = "v1";
        };
        opt.ShortSchemaNames = true;
        opt.AutoTagPathSegmentIndex = 2;
    });
builder.Services.RegisterApplicationServices(builder.Configuration);
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(conf =>
    {
        conf.WithOrigins("http://localhost:4200");
        conf.AllowCredentials().AllowAnyMethod().AllowAnyHeader();
    });
});

var app = builder.Build();

app.UseAuthentication()
    .UseAuthorization()
    .UseFastEndpoints()
    .UseSwaggerGen(uiConfig: opt =>
    {
        // This removes the botton 'Models' section from the swagger UI
        opt.DefaultModelsExpandDepth = -1;
    });

app.UseCors();

app.Run();

public partial class Program { }
