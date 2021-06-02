namespace Xi.BlazorApp.Stores.Features.Game.Actions.EndGame
{
  using Xi.BlazorApp.Models;
  using Xi.Models.Game;

  public class ForfeitAction
  {
    public ForfeitAction(GameModel gameModel, Player player)
    {
      this.GameModel = gameModel;
      this.Player = player;
    }

    public GameModel GameModel { get; }

    public Player Player { get; }
  }
}