namespace Xi.BlazorApp.Stores.Features.Game.Actions.LoadGame
{
  using Xi.BlazorApp.Models;

  public class LoadGameSuccessAction
  {
    public LoadGameSuccessAction(GameViewModel gameViewModel)
    {
      this.GameViewModel = gameViewModel;
    }

    public GameViewModel GameViewModel { get; }
  }
}