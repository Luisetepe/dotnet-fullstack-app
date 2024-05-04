using System.Text.Json;
using Cocona;
using Microsoft.Extensions.Configuration;
using WebApp.Template.Tools.Modules.Seeding;

var builder = CoconaApp.CreateBuilder();
builder.Configuration.AddJsonFile("appsettings.json", false);

var app = builder.Build();
app.AddCommand(
    "seed",
    async (IConfiguration configuration) =>
    {
        Console.Write(
            "Configured database is going to be destroyed and re-created. Are you sure? (y/n): "
        );

        var key = Console.ReadKey();
        Console.Read();

        Console.WriteLine();
        if (key.Key != ConsoleKey.Y)
        {
            Console.WriteLine("Aborting...");
            return;
        }

        var connectionString = configuration.GetValue<string>("WebAppDb");

        if (string.IsNullOrWhiteSpace(connectionString))
        {
            Console.WriteLine("Connection string not found in appsettings.json");
            return;
        }

        var response = await SeedingModule.Run(connectionString);

        Console.WriteLine(
            response.IsSuccess
                ? "Seeding successful"
                : $"Errors:\n{JsonSerializer.Serialize(response.Errors)}"
        );
    }
);
app.Run();
