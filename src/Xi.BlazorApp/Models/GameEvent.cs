namespace Xi.BlazorApp.Models;

public class GameEvent
{
  protected GameEvent(GameModel gameModel)
  {
    this.GameModel = gameModel;
  }

  public GameModel GameModel { get; }
}