namespace Xi.BlazorApp.Stores.Features.Account.Reducers
{
  using Fluxor;
  using Xi.BlazorApp.Stores.Features.Account.Actions.LoadAccount;
  using Xi.BlazorApp.Stores.States;

  public class LoadAccountReducers
  {
    [ReducerMethod]
    public static AccountState Reduce(AccountState state, LoadAccountAction action)
    {
      return new(true, null, null);
    }

    [ReducerMethod]
    public static AccountState Reduce(AccountState state, LoadAccountSuccessAction action)
    {
      return new(false, null, action.Player);
    }
  }
}