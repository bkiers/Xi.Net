namespace Xi.BlazorApp.Stores.Features.Account.Actions.LoadAccount;

using Xi.Models.Game;

public class LoadAccountSuccessAction
{
  public LoadAccountSuccessAction(Player player)
  {
    this.Player = player;
  }

  public Player Player { get; }
}