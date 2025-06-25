using Microsoft.EntityFrameworkCore;
using Tournament.API.Extensions;
using Tournament.Data.Data;
namespace Tournament.API;
public class Program
{
    public static async Task Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        builder.Services.AddDbContext<TournamentAPIContext>(options =>
            options.UseSqlServer(builder.Configuration.GetConnectionString("TournamentAPIContext") ?? throw new InvalidOperationException("Connection string 'TournamentAPIContext' not found.")));
        builder.Services.AddControllers();
        builder.Services.AddOpenApi();
        builder.Services.AddControllers(opt => opt.ReturnHttpNotAcceptable = true)
            .AddNewtonsoftJson(options => 
            options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore)
            .AddXmlDataContractSerializerFormatters();

        var app = builder.Build();
        if (app.Environment.IsDevelopment())
        {
            app.MapOpenApi();
            await app.SeedDataAsync();
        }
        app.UseHttpsRedirection();
        app.UseAuthorization();
        app.MapControllers();
        app.Run();
    }
}
