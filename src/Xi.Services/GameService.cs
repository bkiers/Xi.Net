namespace Xi.Services
{
  using System.Collections.Generic;
  using System.Linq;
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
      return this.db.Games.Select(g => g.AsGame()).ToList();
    }
  }
}