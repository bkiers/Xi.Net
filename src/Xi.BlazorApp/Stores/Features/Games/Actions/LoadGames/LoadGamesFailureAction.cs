namespace Xi.BlazorApp.Stores.Features.Games.Actions.LoadGames
{
  using Xi.BlazorApp.Stores.Features.Shared.Actions;

  public class LoadGamesFailureAction : FailureAction
  {
    public LoadGamesFailureAction(string errorMessage)
      : base(errorMessage)
    {
    }
  }
}