namespace Xi.BlazorApp.Stores.Features.Game.Actions.StartGame
{
  using Xi.BlazorApp.Models;
  using Xi.BlazorApp.Stores.Features.Shared.Actions;
  using Xi.Models.Game;

  public class DeclineGameAction : FailureAction
  {
    public DeclineGameAction(GameModel gameModel, Player player)
      : base("The game is removed.")
    {
      this.GameModel = gameModel;
      this.Player = player;
    }

    public GameModel GameModel { get; }

    public Player Player { get; }
  }
}