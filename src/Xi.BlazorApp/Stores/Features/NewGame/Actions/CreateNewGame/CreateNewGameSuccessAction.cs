namespace Xi.BlazorApp.Stores.Features.NewGame.Actions.CreateNewGame
{
  using Xi.BlazorApp.Models;

  public class CreateNewGameSuccessAction
  {
    public CreateNewGameSuccessAction(GameViewModel gameViewModel)
    {
      this.GameViewModel = gameViewModel;
    }

    public GameViewModel GameViewModel { get; }
  }
}