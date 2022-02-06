namespace Xi.BlazorApp.Stores.Features.Players;

using Fluxor;
using Xi.BlazorApp.Stores.Features.Players.Actions;
using Xi.BlazorApp.Stores.States;

public class PlayersReducers
{
  [ReducerMethod]
  public PlayersState Reduce(PlayersState state, GetPlayersAction action) =>
    state with { IsLoading = true };

  [ReducerMethod]
  public PlayersState Reduce(PlayersState state, GetPlayersResult result) =>
    state with { IsLoading = false, Players = result.Players };
}