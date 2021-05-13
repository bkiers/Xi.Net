namespace Xi.BlazorApp.Stores.Features.Games
{
  using System.Collections.Generic;
  using Fluxor;
  using Xi.BlazorApp.Stores.States;
  using Xi.Models.Game;

  public class GamesFeature : Feature<GamesState>
  {
    public override string GetName() => "Games";

    protected override GamesState GetInitialState() => new GamesState(false, null, new List<Game>());
  }
}