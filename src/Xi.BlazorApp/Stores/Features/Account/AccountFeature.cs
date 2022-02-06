namespace Xi.BlazorApp.Stores.Features.Account;

using Fluxor;
using Xi.BlazorApp.Stores.States;

public class AccountFeature : Feature<AccountState>
{
  public override string GetName() => "Account";

  protected override AccountState GetInitialState() => new(false, null, null);
}