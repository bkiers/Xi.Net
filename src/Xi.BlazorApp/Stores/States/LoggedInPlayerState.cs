namespace Xi.BlazorApp.Stores.States
{
  using Xi.Models.Game;

  public class LoggedInPlayerState : RootState
  {
    public LoggedInPlayerState(bool isLoading, Player? player)
      : base(isLoading, null)
    {
      this.Player = player;
    }

    public Player? Player { get; }
  }
}