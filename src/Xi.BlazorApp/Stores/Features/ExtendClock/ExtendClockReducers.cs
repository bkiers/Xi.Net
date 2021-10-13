namespace Xi.BlazorApp.Stores.Features.ExtendClock
{
  using Fluxor;
  using Xi.BlazorApp.Stores.Features.ExtendClock.Actions;
  using Xi.BlazorApp.Stores.States;

  public class ExtendClockReducers
  {
    [ReducerMethod]
    public ExtendClockState Reduce(ExtendClockState state, GetExtendClockAction action) =>
      state with { IsLoading = true };

    [ReducerMethod]
    public ExtendClockState Reduce(ExtendClockState state, GetExtendClockResult result) =>
      state with { IsLoading = false, IsPossible = result.IsPossible };
  }
}