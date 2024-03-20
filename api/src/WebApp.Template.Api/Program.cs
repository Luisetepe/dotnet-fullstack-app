using FastEndpoints;
using FastEndpoints.Swagger;
using WebApp.Template.Application;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddFastEndpoints().SwaggerDocument(opt =>
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
    options.AddDefaultPolicy(
        conf =>
        {
            conf.AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader();
        });
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
}

app.UseFastEndpoints()
    .UseSwaggerGen(uiConfig: opt =>
    {
        opt.DefaultModelsExpandDepth = -1;
    });
app.UseCors();

app.Run();

public partial class Program
{
}