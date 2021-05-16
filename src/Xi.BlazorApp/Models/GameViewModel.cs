namespace Xi.BlazorApp.Models
{
  using System;
  using System.Collections.Generic;
  using Xi.Models.Game;

  public class GameViewModel
  {
    public GameViewModel(Game game)
    {
      this.Game = game;
      this.FirstClick = null;
    }

    public Game Game { get; }

    public Cell? FirstClick { get; protected set; }

    public void Click(Cell cell)
    {
      var turnColor = this.Game.TurnPlayerColor();

      switch (this.FirstClick)
      {
        case null when !cell.OccupiedBy(turnColor):
          throw new Exception($"It's {this.Game.TurnPlayer.Name}'s turn.");
        case null:
          this.FirstClick = cell;
          break;
        default:
        {
          if (this.FirstClick.Equals(cell))
          {
            // The user clicked the same cell: this means undo the first click.
            this.FirstClick = null;
          }
          else
          {
            this.Game.Move(new Move(this.FirstClick, cell));
            this.FirstClick = null;
          }

          break;
        }
      }
    }

    public ISet<Cell> HighlightedCells()
    {
      var set = new HashSet<Cell>();

      if (this.FirstClick == null)
      {
        return set;
      }

      set.Add(this.FirstClick);
      set.UnionWith(this.Game.ValidToCells(this.FirstClick));

      return set;
    }

    public void UndoFirstClick()
    {
      this.FirstClick = null;
    }
  }
}