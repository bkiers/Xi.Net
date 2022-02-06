namespace Xi.BlazorApp.Stores.Features.ExtendClock;

using Fluxor;
using Xi.BlazorApp.Stores.States;

public class ExtendClockFeature : Feature<ExtendClockState>
{
  public override string GetName() => "ExtendClock";

  protected override ExtendClockState GetInitialState() => new();
}