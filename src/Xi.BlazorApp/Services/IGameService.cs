namespace Xi.BlazorApp.Services
{
  using System.Collections.Generic;
  using Xi.BlazorApp.Models;
  using Xi.Models.Game;

  public interface IGameService
  {
    public List<GameViewModel> Games();

    public GameViewModel? Game(int gameId);

    GameViewModel? NewGame(int loggedInPlayerId, int opponentPlayerId, Color loggedInPlayerColor, int daysPerMove);
  }
}
