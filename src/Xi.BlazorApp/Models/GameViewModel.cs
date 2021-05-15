namespace Xi.BlazorApp.Models
{
  using Xi.Models.Game;

  public class GameViewModel
  {
    public GameViewModel(Game game)
    {
      this.Game = game;
    }

    public Game Game { get; }
  }
}