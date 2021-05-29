namespace Xi.BlazorApp.Stores.Features.Account.Actions.UpdateAccount
{
  using Xi.Models.Game;

  public class UpdateAccountSuccessAction
  {
    public UpdateAccountSuccessAction(Player player)
    {
      this.Player = player;
    }

    public Player Player { get; }
  }
}