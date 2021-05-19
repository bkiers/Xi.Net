namespace Xi.BlazorApp.Services
{
  using System;
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

    public GameModel Accept(int loggedInPlayerId, int gameId)
    {
      var game = this.db.Games
        .Include(g => g.InvitedPlayer)
        .Single(g => g.Id == gameId);

      if (game.InvitedPlayer.Id != loggedInPlayerId)
      {
        throw new Exception($"Only {game.InvitedPlayer.Name} can accept this game.");
      }

      game.Accepted = true;
      game.ClockRunsOutAt = DateTime.UtcNow + TimeSpan.FromSeconds(game.SecondsPerMove);

      this.db.SaveChanges();

      return new GameModel(game.ToGame());
    }

    public bool Decline(int loggedInPlayerId, int gameId)
    {
      // TODO
      throw new NotImplementedException();
    }

    public List<GameModel> Games()
    {
      var games = this.db.Games
        .Include(g => g.RedPlayer)
        .Include(g => g.BlackPlayer)
        .Select(g => new GameModel(g.ToGame()))
        .ToList();

      return games;
    }

    public GameModel? Game(int gameId)
    {
      var game = this.db.Games
        .Include(g => g.RedPlayer)
        .Include(g => g.BlackPlayer)
        .Include(g => g.TurnPlayer)
        .Include(g => g.InitiatedPlayer)
        .Include(g => g.InvitedPlayer)
        .Include(g => g.Moves.OrderBy(m => m.CreatedAt))
        .SingleOrDefault(g => g.Id == gameId)?
        .ToGame();

      return game == null ? null : new GameModel(game);
    }

    public GameModel? NewGame(int loggedInPlayerId, int opponentPlayerId, Color loggedInPlayerColor, int daysPerMove)
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

    public GameModel? Move(int loggedInPlayerId, int gameId, Cell fromCell, Cell toCell)
    {
      var game = this.db.Games
        .Include(g => g.TurnPlayer)
        .Include(g => g.InvitedPlayer)
        .Single(g => g.Id == gameId);

      if (!game.Accepted)
      {
        throw new Exception($"{game.InvitedPlayer.Name} first needs to accept this game.");
      }

      if (game.TurnPlayerId != loggedInPlayerId)
      {
        throw new Exception($"It's {game.TurnPlayer.Name}'s turn.");
      }

      var move = new MoveDto
      {
        GameId = gameId,
        FromFileIndex = fromCell.FileIndex,
        FromRankIndex = fromCell.RankIndex,
        ToFileIndex = toCell.FileIndex,
        ToRankIndex = toCell.RankIndex,
      };

      using var transaction = this.db.Database.BeginTransaction();

      this.db.Moves.Add(move);

      game.TurnPlayerId = game.RedPlayerId == loggedInPlayerId ? game.BlackPlayerId : game.RedPlayerId;

      this.db.SaveChanges();
      transaction.Commit();

      return this.Game(gameId);
    }
  }
}