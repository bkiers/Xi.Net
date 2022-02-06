namespace Xi.BlazorApp.Stores.Features.NewGame;

using Fluxor;
using Xi.BlazorApp.Stores.States;

public class NewGameFeature : Feature<NewGameState>
{
  public override string GetName() => "NewGame";

  protected override NewGameState GetInitialState() => new(false, null, null);
}