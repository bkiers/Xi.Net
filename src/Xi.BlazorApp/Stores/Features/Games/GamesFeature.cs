namespace Xi.BlazorApp.Stores.Features.Games
{
  using Fluxor;
  using Xi.BlazorApp.Stores.States;

  public class GamesFeature : Feature<GamesState>
  {
    public override string GetName() => "Games";

    protected override GamesState GetInitialState() => new(false, null, null);
  }
}