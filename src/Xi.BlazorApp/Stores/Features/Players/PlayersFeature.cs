namespace Xi.BlazorApp.Stores.Features.Players;

using Fluxor;
using Xi.BlazorApp.Stores.States;

public class PlayersFeature : Feature<PlayersState>
{
  public override string GetName() => "Players";

  protected override PlayersState GetInitialState() => new();
}