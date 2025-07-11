﻿namespace Tournament.Core.Entities;
public class Game
{
    public int Id { get; set; }
    public string? Title { get; set; }
    public DateTime Time { get; set; }
    public int TournamentId { get; set; }
    public required TournamentDetails Tournament { get; set; }
}
