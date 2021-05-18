namespace Xi.BlazorApp.Stores.Features.LoggedInPlayer
{
  using Fluxor;
  using Xi.BlazorApp.Stores.States;

  public class LoggedInPlayerFeature : Feature<LoggedInPlayerState>
  {
    public override string GetName() => "LoggedInPlayer";

    protected override LoggedInPlayerState GetInitialState() => new(false, null);
  }
}