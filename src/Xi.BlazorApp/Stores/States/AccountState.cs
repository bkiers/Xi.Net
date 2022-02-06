namespace Xi.BlazorApp.Stores.States;

using Xi.Models.Game;

public class AccountState : RootState
{
  public AccountState(bool isLoading, string? errorMessage, Player? player)
    : base(isLoading, errorMessage)
  {
    this.Player = player;
  }

  public Player? Player { get; }
}