namespace Xi.BlazorApp.Stores.Features.NewGame.Actions.CreateNewGame
{
  using Xi.BlazorApp.Stores.Features.Shared.Actions;

  public class CreateNewGameFailureAction : FailureAction
  {
    public CreateNewGameFailureAction(string errorMessage)
      : base(errorMessage)
    {
    }
  }
}