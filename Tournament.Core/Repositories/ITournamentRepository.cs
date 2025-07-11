﻿using Tournament.Core.Entities;

namespace Tournament.Core.Repositories;
public interface ITournamentRepository
{
    Task<IEnumerable<TournamentDetails>> GetAllAsync(bool includeGames);
    Task<TournamentDetails> GetAsync(int id);
    Task<bool> AnyAsync(int id);
    void Add(TournamentDetails tournament);
    void Update(TournamentDetails tournament);
    void Remove(int id);
}
