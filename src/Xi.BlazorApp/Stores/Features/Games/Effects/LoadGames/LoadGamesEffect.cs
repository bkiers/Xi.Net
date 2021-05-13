namespace Xi.BlazorApp.Stores.Features.Games.Effects.LoadGames
{
  using System;
  using System.Collections.Generic;
  using System.Threading.Tasks;
  using Fluxor;
  using Xi.BlazorApp.Stores.Features.Games.Actions.LoadGames;
  using Xi.Models.Game;

  public class LoadGamesEffect : Effect<LoadGamesAction>
  {
    // TODO inject GameService or just a DB context

    public override async Task HandleAsync(LoadGamesAction action, IDispatcher dispatcher)
    {
      await Task.Delay(TimeSpan.FromMilliseconds(3000));

      var games = new List<Game>
      {
        new(),
        new(),
        new(),
      };

      dispatcher.Dispatch(new LoadGamesSuccessAction(games));
    }
  }
}