namespace Xi.BlazorApp.Services
{
  using System.Collections.Generic;
  using System.Linq;
  using Microsoft.EntityFrameworkCore;
  using Xi.Database;
  using Xi.Models.Game;

  public class GameService : IGameService
  {
    private readonly XiContext db;

    public GameService(XiContext db)
    {
      this.db = db;
    }

    public List<Game> Games()
    {
      var games = this.db.Games
        .Include(g => g.RedPlayer)
        .Include(g => g.BlackPlayer)
        .Select(g => g.ToGame())
        .ToList();

      return games;
    }

    public Game? Game(int gameId)
    {
      // TODO: load moves
      var game = this.db.Games
        .SingleOrDefault(g => g.Id == gameId)?
        .ToGame();

      return game;
    }
  }
}