namespace Xi.BlazorApp.Stores.Features.Account.Actions.UpdateAccount
{
  using Xi.BlazorApp.Models;

  public class UpdateAccountAction
  {
    public UpdateAccountAction(int playerId, bool showPossibleMoves)
    {
      this.PlayerId = playerId;
      this.ShowPossibleMoves = showPossibleMoves;
    }

    public int PlayerId { get; }

    public bool ShowPossibleMoves { get; }
  }
}