namespace Xi.BlazorApp.Stores.Features.Game.Actions.LoadGame
{
  public class LoadGameAction
  {
    public LoadGameAction(int gameId)
    {
      this.GameId = gameId;
    }

    public int GameId { get; }
  }
}