namespace Xi.BlazorApp.Stores.Features.Game;

using Fluxor;
using Xi.BlazorApp.Stores.States;

public class GameFeature : Feature<GameState>
{
  public override string GetName() => "Game";

  protected override GameState GetInitialState() => new(false, null, null);
}