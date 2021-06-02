namespace Xi.BlazorApp.Services
{
  using System;
  using System.Collections.Generic;
  using System.Linq;
  using JKang.EventBus;
  using Microsoft.EntityFrameworkCore;
  using Xi.BlazorApp.EventHandlers;
  using Xi.BlazorApp.Models;
  using Xi.Database;
  using Xi.Database.Dtos;
  using Xi.Models.Extensions;
  using Xi.Models.Game;

  public class GameService : IGameService
  {
    private readonly XiContext db;
    private readonly IEventPublisher eventPublisher;

    public GameService(XiContext db, IEventPublisher eventPublisher)
    {
      this.db = db;
      this.eventPublisher = eventPublisher;
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

      var gameModel = this.Game(game.Id)!;

      this.eventPublisher.PublishEventAsync(new AcceptNewGameEventHandler.Event(gameModel));

      return gameModel;
    }

    public GameModel ProposeDraw(int loggedInPlayerId, int gameId)
    {
      var game = this.db.Games
        .Include(g => g.Moves.OrderBy(m => m.CreatedAt))
        .AsSplitQuery()
        .Single(g => g.Id == gameId);

      if (game.ProposedDrawPlayerId != null)
      {
        throw new Exception("There is already a pending draw proposal.");
      }

      game.ProposedDrawPlayerId = loggedInPlayerId;
      this.db.SaveChanges();

      var gameModel = this.Game(game.Id)!;

      this.eventPublisher.PublishEventAsync(new DrawProposalEventHandler.Event(gameModel));

      return gameModel;
    }

    public GameModel HandleDrawProposal(int loggedInPlayerId, int gameId, bool accept)
    {
      var game = this.db.Games.Single(g => g.Id == gameId);

      if (game.ProposedDrawPlayerId == null)
      {
        throw new Exception("There is no pending draw proposal.");
      }

      if (game.ProposedDrawPlayerId == loggedInPlayerId)
      {
        throw new Exception("You cannot accept your own draw proposal.");
      }

      game.ProposedDrawPlayerId = null;

      if (accept)
      {
        game.AcceptedDrawPlayerId = loggedInPlayerId;
      }
      else
      {
        this.eventPublisher.PublishEventAsync(new DeclineDrawProposalEventHandler.Event(this.Game(gameId)!));
      }

      this.db.SaveChanges();

      return accept ? this.EndGame(gameId, null, GameResultType.Draw) : this.Game(game.Id)!;
    }

    public bool Decline(int loggedInPlayerId, int gameId)
    {
      var game = this.db.Games
        .Include(g => g.InvitedPlayer)
        .Single(g => g.Id == gameId);

      if (game.InvitedPlayer.Id != loggedInPlayerId)
      {
        throw new Exception($"Only {game.InvitedPlayer.Name} can decline this game.");
      }

      var gameModel = this.Game(game.Id)!;
      this.db.Games.Remove(game);

      this.eventPublisher.PublishEventAsync(new DeclineNewGameEventHandler.Event(gameModel));

      return this.db.SaveChanges() == 1;
    }

    // TODO: calculate ELO change
    public GameModel EndGame(int gameId, int? winnerPlayerId, GameResultType gameResultType)
    {
      var game = this.db.Games.Single(g => g.Id == gameId);

      game.WinnerPlayerId = winnerPlayerId;
      game.GameResultType = gameResultType;

      var gameModel = this.Game(game.Id)!;

      this.eventPublisher.PublishEventAsync(new GameOverEventHandler.Event(gameModel));

      this.db.SaveChanges();

      return this.Game(game.Id)!;
    }

    public List<GameModel> Games()
    {
      var games = this.db.Games
        .OrderByDescending(g => g.Id)
        .Include(g => g.RedPlayer)
        .Include(g => g.BlackPlayer)
        .Select(g => new GameModel(g.ToGame()))
        .ToList();

      return games;
    }

    public List<GameModel> UnfinishedGames()
    {
      var games = this.db.Games
        .Where(g => !g.GameResultType.HasValue && g.Accepted)
        .Include(g => g.RedPlayer)
        .Include(g => g.BlackPlayer)
        .Include(g => g.Reminders)
        .AsSplitQuery()
        .Include(g => g.Moves.OrderBy(m => m.CreatedAt))
        .AsSplitQuery()
        .Select(g => new GameModel(g.ToGame()))
        .ToList();

      return games;
    }

    public GameModel? Game(int gameId)
    {
      var game = this.db.Games
        .Include(g => g.RedPlayer)
        .Include(g => g.BlackPlayer)
        .Include(g => g.InitiatedPlayer)
        .Include(g => g.InvitedPlayer)
        .Include(g => g.Moves.OrderBy(m => m.CreatedAt))
        .AsSplitQuery()
        .SingleOrDefault(g => g.Id == gameId)?
        .ToGame();

      return game == null ? null : new GameModel(game);
    }

    public GameModel NewGame(int loggedInPlayerId, int opponentPlayerId, Color loggedInPlayerColor, int daysPerMove)
    {
      var game = new GameDto
      {
        InitiatedPlayerId = loggedInPlayerId,
        InvitedPlayerId = opponentPlayerId,
        RedPlayerId = loggedInPlayerColor == Color.Red ? loggedInPlayerId : opponentPlayerId,
        BlackPlayerId = loggedInPlayerColor == Color.Black ? loggedInPlayerId : opponentPlayerId,
        SecondsPerMove = daysPerMove * 24 * 60 * 60,
      };

      this.db.Games.Add(game);
      this.db.SaveChanges();

      var gameModel = this.Game(game.Id)!;

      this.eventPublisher.PublishEventAsync(new NewGameEventHandler.Event(gameModel));

      return gameModel;
    }

    public GameModel Move(int loggedInPlayerId, int gameId, Cell fromCell, Cell toCell)
    {
      var game = this.db.Games
        .Include(g => g.InvitedPlayer)
        .Include(g => g.RedPlayer)
        .Include(g => g.BlackPlayer)
        .Include(g => g.Moves.OrderBy(m => m.CreatedAt))
        .AsSplitQuery()
        .Single(g => g.Id == gameId);

      if (game.GameResultType.HasValue)
      {
        throw new Exception($"Game {game.Id} is already over.");
      }

      if (!game.Accepted)
      {
        throw new Exception($"{game.InvitedPlayer.Name} first needs to accept this game.");
      }

      if (game.TurnPlayerId() != loggedInPlayerId)
      {
        throw new Exception($"It's {game.TurnPlayer().Name}'s turn.");
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

      var gameModel = this.Game(gameId)!;
      var colorMoved = loggedInPlayerId == game.RedPlayerId ? Color.Red : Color.Black;

      var checkmate = gameModel.Game.CurrentBoard().IsCheckmate(colorMoved.Opposite());
      var stalemate = gameModel.Game.CurrentBoard().IsStalemate(colorMoved.Opposite());

      if (checkmate || stalemate)
      {
        game.WinnerPlayerId = loggedInPlayerId;
        game.ClockRunsOutAt = null;
        game.GameResultType = checkmate ? GameResultType.Checkmate : GameResultType.Stalemate;
      }
      else
      {
        game.ClockRunsOutAt = DateTime.UtcNow + TimeSpan.FromSeconds(game.SecondsPerMove);
      }

      // In case there was a draw-proposal, revoke it.
      game.ProposedDrawPlayerId = null;

      this.db.SaveChanges();

      transaction.Commit();

      var reloadedGameModel = new GameModel(game.ToGame());

      if (checkmate || stalemate)
      {
        this.eventPublisher.PublishEventAsync(new GameOverEventHandler.Event(reloadedGameModel));
      }
      else
      {
        this.eventPublisher.PublishEventAsync(new MoveMadeEventHandler.Event(reloadedGameModel));
      }

      return reloadedGameModel;
    }
  }
}