namespace Xi.BlazorApp.Stores.Features.Account.Actions.UpdateAccount
{
  using Xi.BlazorApp.Models;

  public class UpdateAccountAction
  {
    public UpdateAccountAction(UpdateAccountModel updateAccountModel)
    {
      this.UpdateAccountModel = updateAccountModel;
    }

    public UpdateAccountModel UpdateAccountModel { get; }
  }
}