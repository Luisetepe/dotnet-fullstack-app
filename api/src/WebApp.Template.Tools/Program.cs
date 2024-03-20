using Cocona;
using Microsoft.Extensions.Configuration;
using WebApp.Template.Tools.Modules.Seeding;

var builder = CoconaApp.CreateBuilder();
builder.Configuration.AddJsonFile("appsettings.json", false);

var app = builder.Build();
app.AddCommand("seed", async (IConfiguration configuration) =>
{
    Console.Write("Configured database is going to be destroyed and re-created. Are you sure? (y/n): ");

    var key = Console.ReadKey();
    Console.Read();

    if (key.Key != ConsoleKey.Y)
    {
        Console.WriteLine();
        Console.WriteLine("Aborting...");
        return;
    }

    var response = await SeedingModule.Run(configuration.GetValue<string>("WebAppDb") ?? throw new ArgumentNullException());
    Console.WriteLine(response.Message);
});
app.Run();