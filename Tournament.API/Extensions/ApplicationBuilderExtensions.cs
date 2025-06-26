using Bogus;
using Microsoft.EntityFrameworkCore;
using Tournament.Core.Entities;
using Tournament.Data.Data;

namespace Tournament.API.Extensions;
public static class ApplicationBuilderExtensions
{
    public static async Task SeedDataAsync(this IApplicationBuilder builder)
    {
        using var scope = builder.ApplicationServices.CreateScope();
        var serviceProvider = scope.ServiceProvider;
        var db = serviceProvider.GetRequiredService<TournamentAPIContext>();
        await db.Database.MigrateAsync();
        if (await db.TournamentDetails.AnyAsync())
        {
            return; // Database has been seeded
        }

        try
        {
            var tournamentDetails = GenerateTournamentDetails(4);
            db.AddRange(tournamentDetails);
            await db.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    private static List<TournamentDetails> GenerateTournamentDetails(int nrOfTournamentDetails)
    {
        var faker = new Faker<TournamentDetails>("sv")
            .RuleFor(t => t.Title, f => f.Company.CompanyName())
            .RuleFor(t => t.StartDate, f => f.Date.Future())
            .RuleFor(t => t.Games, (f, t) =>
            {
                var games = GenerateGames(t, f.Random.Int(2, 5)).ToList();
                t.Games = games;  // Explicitly assign generated games to the tournament
                return games;
            });

        return faker.Generate(nrOfTournamentDetails);
    }

    private static ICollection<Game> GenerateGames(TournamentDetails tournament, int nrOfGames)
    {
        var faker = new Faker<Game>("sv")
            .RuleFor(g => g.Title, f => f.Commerce.ProductName())
            .RuleFor(g => g.Time, f => f.Date.Soon())
            .RuleFor(g => g.Tournament, f => tournament)
            .RuleFor(g => g.TournamentId, 0);

        return faker.Generate(nrOfGames);
    }
}
