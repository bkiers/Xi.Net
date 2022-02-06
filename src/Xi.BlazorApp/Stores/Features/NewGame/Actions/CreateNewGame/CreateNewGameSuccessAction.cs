namespace Xi.BlazorApp.Stores.Features.NewGame.Actions.CreateNewGame;

using Xi.BlazorApp.Models;

public class CreateNewGameSuccessAction
{
  public CreateNewGameSuccessAction(GameModel gameModel)
  {
    this.GameModel = gameModel;
  }

  public GameModel GameModel { get; }
}