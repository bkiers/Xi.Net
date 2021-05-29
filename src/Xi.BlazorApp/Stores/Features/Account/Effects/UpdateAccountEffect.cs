namespace Xi.BlazorApp.Stores.Features.Account.Effects
{
  using System.Threading.Tasks;
  using Fluxor;
  using Xi.BlazorApp.Services;
  using Xi.BlazorApp.Stores.Features.Account.Actions.UpdateAccount;

  public class UpdateAccountEffect : Effect<UpdateAccountAction>
  {
    private readonly IPlayerService playerService;

    public UpdateAccountEffect(IPlayerService playerService)
    {
      this.playerService = playerService;
    }

    public override Task HandleAsync(UpdateAccountAction action, IDispatcher dispatcher)
    {
      var player = this.playerService.Update(action.PlayerId, action.ShowPossibleMoves);

      dispatcher.Dispatch(new UpdateAccountSuccessAction(player));

      return Task.CompletedTask;
    }
  }
}