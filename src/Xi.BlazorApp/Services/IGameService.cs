namespace Xi.BlazorApp.Services
{
  using System.Collections.Generic;
  using Xi.BlazorApp.Models;
  using Xi.Models.Game;

  public interface IGameService
  {
    GameModel Accept(int loggedInPlayerId, int gameId);

    GameModel ProposeDraw(int loggedInPlayerId, int gameId);

    GameModel HandleDrawProposal(int loggedInPlayerId, int gameId, bool accept);

    bool Decline(int loggedInPlayerId, int gameId);

    GameModel EndGame(int gameId, int? winnerPlayerId, GameResultType gameResultType);

    List<GameModel> Games();

    List<GameModel> UnfinishedGames();

    GameModel? Game(int gameId);

    GameModel? NewGame(int loggedInPlayerId, int opponentPlayerId, Color loggedInPlayerColor, int daysPerMove);

    GameModel? Move(int loggedInPlayerId, int gameId, Cell fromCell, Cell toCell);
  }
}
