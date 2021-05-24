namespace Xi.BlazorApp.Stores.Features.Game.Effects
{
  using System;
  using System.Threading.Tasks;
  using Fluxor;
  using Microsoft.AspNetCore.Components;
  using Xi.BlazorApp.Extensions;
  using Xi.BlazorApp.Hubs;
  using Xi.BlazorApp.Services;
  using Xi.BlazorApp.Stores.Features.Game.Actions.Moves;

  public class ConfirmMoveEffect : Effect<ConfirmMoveAction>
  {
    private readonly IGameService gameService;
    private readonly NavigationManager navigationManager;

    public ConfirmMoveEffect(IGameService gameService, NavigationManager navigationManager)
    {
      this.gameService = gameService;
      this.navigationManager = navigationManager;
    }

    public override async Task HandleAsync(ConfirmMoveAction action, IDispatcher dispatcher)
    {
      try
      {
        var move = action.GameModel.Game.Moves[action.Index];
        var game = this.gameService.Move(action.LoggedInUserId, action.GameModel.Game.Id, move.FromCell, move.ToCell);

        dispatcher.Dispatch(new ConfirmMoveSuccessAction(game!));
      }
      catch (Exception e)
      {
        dispatcher.Dispatch(new ConfirmMoveFailureAction(action.GameModel, e.Message));
      }
      finally
      {
        var hubConnection = GamesHub.Connection(this.navigationManager.BaseUri);

        await hubConnection.StartSendStopAsync(EventTypes.MoveMade.ToString(), action.GameModel.Game.Id);
      }
    }
  }
}