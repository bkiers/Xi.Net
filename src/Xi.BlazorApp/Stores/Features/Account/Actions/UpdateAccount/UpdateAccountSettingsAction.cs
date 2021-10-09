namespace Xi.BlazorApp.Stores.Features.Account.Actions.UpdateAccount
{
  using Xi.Models;

  public class UpdateAccountSettingsAction
  {
    public UpdateAccountSettingsAction(int playerId, Settings settings)
    {
      this.PlayerId = playerId;
      this.Settings = settings;
    }

    public int PlayerId { get; }

    public Settings Settings { get; }
  }
}