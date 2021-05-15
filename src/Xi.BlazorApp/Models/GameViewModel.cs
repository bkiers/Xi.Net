namespace Xi.BlazorApp.Models
{
  using System;
  using Xi.Models.Game;

  public class GameViewModel
  {
    public GameViewModel(Game game)
    {
      this.Game = game;
    }

    public Game Game { get; }


    public void Click(Cell cell)
    {
      var turnColor = this.Game.TurnPlayerColor();

      if (!cell.OccupiedBy(turnColor))
      {
        throw new Exception($"It's {this.Game.TurnPlayer.Name}'s turn.");
      }

      // TODO
    }
  }
}