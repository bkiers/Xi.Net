namespace Xi.BlazorApp.Services
{
  using System.Collections.Generic;
  using Xi.BlazorApp.Models;
  using Xi.Models.Game;

  public interface IGameService
  {
    public List<GameModel> Games();

    public GameModel? Game(int gameId);

    GameModel? NewGame(int loggedInPlayerId, int opponentPlayerId, Color loggedInPlayerColor, int daysPerMove);
  }
}
