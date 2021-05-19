namespace Xi.BlazorApp.Services
{
  using System.Collections.Generic;
  using Xi.BlazorApp.Models;
  using Xi.Models.Game;

  public interface IGameService
  {
    List<GameModel> Games();

    GameModel? Game(int gameId);

    GameModel? NewGame(int loggedInPlayerId, int opponentPlayerId, Color loggedInPlayerColor, int daysPerMove);

    GameModel? Move(int gameId, Cell fromCell, Cell toCell);
  }
}
