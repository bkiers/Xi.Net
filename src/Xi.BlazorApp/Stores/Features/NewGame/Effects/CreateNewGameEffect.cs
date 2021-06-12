namespace Xi.BlazorApp.Stores.Features.NewGame.Effects
{
  using System;
  using System.Threading.Tasks;
  using Fluxor;
  using Xi.BlazorApp.Services;
  using Xi.BlazorApp.Stores.Features.NewGame.Actions.CreateNewGame;

  public class CreateNewGameEffect : Effect<CreateNewGameAction>
  {
    private readonly IGameService gameService;

    public CreateNewGameEffect(IGameService gameService)
    {
      this.gameService = gameService;
    }

    public override Task HandleAsync(CreateNewGameAction action, IDispatcher dispatcher)
    {
      try
      {
        var game = this.gameService.NewGame(
          action.LoggedInPlayerId,
          action.OpponentPlayerId,
          action.PlayingWithColor,
          action.DaysPerMove);

        dispatcher.Dispatch(new CreateNewGameSuccessAction(game!));
      }
      catch (Exception e)
      {
        dispatcher.Dispatch(new CreateNewGameFailureAction(e.Message));
      }

      return Task.CompletedTask;
    }
  }
}