namespace Xi.BlazorApp.Stores.Features.Account.Reducers
{
  using Fluxor;
  using Xi.BlazorApp.Stores.Features.Account.Actions.UpdateAccount;
  using Xi.BlazorApp.Stores.States;

  public class UpdateAccountReducers
  {
    [ReducerMethod]
    public static AccountState Reduce(AccountState state, UpdateAccountSettingsAction settingsAction)
    {
      return new(true, null, null);
    }

    [ReducerMethod]
    public static AccountState Reduce(AccountState state, UpdateAccountSuccessAction action)
    {
      return new(false, null, action.Player);
    }
  }
}