namespace Xi.BlazorApp.Stores.Features.LoggedInPlayer.Actions.ReadLoggedInPlayerFromCookie
{
  using Xi.Models.Game;

  public class ReadLoggedInPlayerFromCookieSuccessAction
  {
    public ReadLoggedInPlayerFromCookieSuccessAction(Player player)
    {
      this.Player = player;
    }

    public Player Player { get; }
  }
}