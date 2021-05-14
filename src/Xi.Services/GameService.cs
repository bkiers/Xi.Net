namespace Xi.Services
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
      return this.db.Games
        .Include(g => g.RedPlayer)
        .Include(g => g.BlackPlayer)
        .Select(g => g.ToGame())
        .ToList();
    }

    public Game Game(int gameId)
    {
      // TODO: load moves
      return this.db.Games
        .Single(g => g.Id == gameId)
        .ToGame();
    }
  }
}