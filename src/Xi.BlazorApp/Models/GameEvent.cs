namespace Xi.BlazorApp.Models
{
  public class GameEvent
  {
    public GameEvent(GameModel gameModel)
    {
      this.GameModel = gameModel;
    }

    public GameModel GameModel { get; }
  }
}