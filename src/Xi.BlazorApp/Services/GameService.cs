namespace Xi.BlazorApp.Services
{
  using System.Collections.Generic;
  using System.Linq;
  using Microsoft.EntityFrameworkCore;
  using Xi.BlazorApp.Models;
  using Xi.Database;
  using Xi.Database.Dtos;
  using Xi.Models.Game;

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


    public GameViewModel? NewGame(int loggedInPlayerId, int opponentPlayerId, Color loggedInPlayerColor, int daysPerMove)
    {
      var game = new GameDto
      {
        InitiatedPlayerId = loggedInPlayerId,
        InvitedPlayerId = opponentPlayerId,
        RedPlayerId = loggedInPlayerColor == Color.Red ? loggedInPlayerId : opponentPlayerId,
        BlackPlayerId = loggedInPlayerColor == Color.Black ? loggedInPlayerId : opponentPlayerId,
        SecondsPerMove = daysPerMove * 24 * 60 * 60,
        TurnPlayerId = loggedInPlayerColor == Color.Red ? loggedInPlayerId : opponentPlayerId,
      };

      this.db.Games.Add(game);
      this.db.SaveChanges();

      return this.Game(game.Id);
    }
  }
}