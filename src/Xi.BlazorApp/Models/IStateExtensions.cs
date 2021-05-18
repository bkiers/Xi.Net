namespace Xi.BlazorApp.Models
{
  using Fluxor;
  using Xi.BlazorApp.Stores.States;
  using Xi.Models.Game;

  public static class IStateExtensions
  {
    public static Player Player(this IState<LoggedInPlayerState> state)
    {
      return state.Value.Player!;
    }

    public static int PlayerId(this IState<LoggedInPlayerState> state)
    {
      return state.Value.Player!.Id;
    }
  }
}