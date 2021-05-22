namespace Xi.BlazorApp.Stores.Features.Game.Effects
{
  using System;
  using System.Net.Http;
  using System.Threading.Tasks;
  using Fluxor;
  using Microsoft.AspNetCore.SignalR.Client;
  using Xi.BlazorApp.Hubs;
  using Xi.BlazorApp.Services;
  using Xi.BlazorApp.Stores.Features.Game.Actions.Moves;

  public class ConfirmMoveEffect : Effect<ConfirmMoveAction>
  {
    private readonly IGameService gameService;

    public ConfirmMoveEffect(IGameService gameService)
    {
      this.gameService = gameService;
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

      await this.Foo(action.GameModel.Game.Id);

      // return Task.CompletedTask;
    }

    private async Task Foo(int gameId)
    {
      var hubConnection = new HubConnectionBuilder()
        .WithUrl($"https://localhost:9900{GamesHub.HubUrl}", options =>
        {
          options.WebSocketConfiguration = conf =>
          {
            conf.RemoteCertificateValidationCallback = (message, cert, chain, errors) => true;
          };
          options.HttpMessageHandlerFactory = factory => new HttpClientHandler
          {
            ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => true,
          };
        })
        .Build();

      await hubConnection.StartAsync();

      await hubConnection.SendAsync("MoveMade", gameId);

      await hubConnection.StopAsync();
    }
  }
}