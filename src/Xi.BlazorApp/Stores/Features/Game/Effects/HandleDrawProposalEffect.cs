namespace Xi.BlazorApp.Stores.Features.Game.Effects
{
  using System;
  using System.Threading.Tasks;
  using Fluxor;
  using Xi.BlazorApp.Services;
  using Xi.BlazorApp.Stores.Features.Game.Actions.DrawGame;

  public class HandleDrawProposalEffect : Effect<HandleDrawProposalAction>
  {
    private readonly IGameService gameService;

    public HandleDrawProposalEffect(IGameService gameService)
    {
      this.gameService = gameService;
    }

    public override Task HandleAsync(HandleDrawProposalAction action, IDispatcher dispatcher)
    {
      try
      {
        var game = this.gameService.HandleDrawProposal(action.Player.Id, action.GameModel.Game.Id, action.Accept);

        dispatcher.Dispatch(new HandleDrawProposalSuccessAction(game));
      }
      catch (Exception e)
      {
        dispatcher.Dispatch(new HandleDrawProposalFailureAction(action.GameModel, e.Message));
      }

      return Task.CompletedTask;
    }
  }
}