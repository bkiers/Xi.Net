namespace Xi.BlazorApp.Stores.Features.Game.Actions.StartGame
{
  using Xi.BlazorApp.Models;
  using Xi.Models.Game;

  public class DeclineGameAction
  {
    public DeclineGameAction(GameModel gameModel, Player player)
    {
      this.GameModel = gameModel;
      this.Player = player;
    }

    public GameModel GameModel { get; }

    public Player Player { get; }
  }
}