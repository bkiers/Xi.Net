namespace Xi.BlazorApp.Stores.Features.Game.Actions.LoadGame
{
  using Xi.BlazorApp.Stores.Features.Shared.Actions;

  public class LoadGameFailureAction : FailureAction
  {
    public LoadGameFailureAction(string errorMessage)
      : base(errorMessage)
    {
    }
  }
}