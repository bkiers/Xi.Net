namespace Xi.BlazorApp.Services
{
  using System.Collections.Generic;
  using System.Linq;
  using Microsoft.EntityFrameworkCore;
  using Xi.BlazorApp.Models;
  using Xi.Database;
  using Xi.Database.Dtos;

  public class GameService : IGameService
  {
    private readonly XiContext db;

    public GameService(XiContext db)
    {
      this.db = db;
    }

    public List<GameViewModel> Games()
    {
      var games = this.db.Games
        .Include(g => g.RedPlayer)
        .Include(g => g.BlackPlayer)
        .Select(g => new GameViewModel(g.ToGame()))
        .ToList();

      return games;
    }

    public GameViewModel? Game(int gameId)
    {
      var game = this.db.Games
        .Include(g => g.RedPlayer)
        .Include(g => g.BlackPlayer)
        .Include(g => g.TurnPlayer)
        .Include(g => g.Moves)
        .SingleOrDefault(g => g.Id == gameId)?
        .ToGame();

      return game == null ? null : new GameViewModel(game);
    }

    public bool NewGame(int initiatedPlayerId, int invitedPlayerId, int redPlayerId, int blackPlayerId, int secondsPerMove)
    {
      var game = new GameDto
      {
        InitiatedPlayerId = initiatedPlayerId,
        InvitedPlayerId = invitedPlayerId,
        RedPlayerId = redPlayerId,
        BlackPlayerId = blackPlayerId,
        SecondsPerMove = secondsPerMove,
      };

      this.db.Games.Add(game);

      return this.db.SaveChanges() == 1;
    }
  }
}