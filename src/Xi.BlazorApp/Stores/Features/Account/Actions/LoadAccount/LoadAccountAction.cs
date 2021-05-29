namespace Xi.BlazorApp.Stores.Features.Account.Actions.LoadAccount
{
  public class LoadAccountAction
  {
    public LoadAccountAction(int playerId)
    {
      this.PlayerId = playerId;
    }

    public int PlayerId { get; }
  }
}