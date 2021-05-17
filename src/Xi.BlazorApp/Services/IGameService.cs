namespace Xi.BlazorApp.Services
{
  using System.Collections.Generic;
  using Xi.BlazorApp.Models;

  public interface IGameService
  {
    public List<GameViewModel> Games();

    public GameViewModel? Game(int gameId);

    public bool NewGame(
      int initiatedPlayerId,
      int invitedPlayerId,
      int redPlayerId,
      int blackPlayerId,
      int secondsPerMove);
  }
}
