namespace Xi.BlazorApp.Stores.Features.NewGame.Effects
{
  using System.Threading.Tasks;
  using Fluxor;
  using Xi.BlazorApp.Stores.Features.NewGame.Actions.CreateNewGame;

  public class CreateNewGameEffect : Effect<CreateNewGameAction>
  {
    public override Task HandleAsync(CreateNewGameAction action, IDispatcher dispatcher)
    {
      throw new System.NotImplementedException();
    }
  }
}