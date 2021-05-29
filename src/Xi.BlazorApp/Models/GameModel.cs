namespace Xi.BlazorApp.Models
{
  using System;
  using System.Collections.Generic;
  using System.Linq;
  using Xi.Models.Game;

  public class GameModel
  {
    public GameModel(Game game)
    {
      this.Game = game;
      this.CurrentMoveIndex = this.Game.Moves.Any() ? this.Game.Moves.Count - 1 : -1;
      this.FirstClick = null;
    }

    public GameModel(Game game, int currentMoveIndex, Cell? firstClick)
    {
      this.Game = game;
      this.CurrentMoveIndex = currentMoveIndex;
      this.FirstClick = firstClick;
    }

    public Game Game { get; }

    public int CurrentMoveIndex { get; private set; }

    public Cell? FirstClick { get; private set; }

    public bool CanBeConfirmed(int index)
    {
      var lastConfirmedMove = this.Game.Moves.FindLast(m => m.CreatedAt.HasValue);
      var indexLastConfirmedMove = lastConfirmedMove == null ? -1 : this.Game.Moves.IndexOf(lastConfirmedMove);

      return indexLastConfirmedMove + 1 == index;
    }

    public Board CurrentBoard()
    {
      // There is always 1 board more than there are moves: the first board is the start-board.
      return this.Game.Board(this.CurrentMoveIndex + 1);
    }

    public void Click(Cell cell)
    {
      if (this.Game.Moves.Count != this.CurrentMoveIndex + 1)
      {
        // Ignore clicks on the board if we're not focussed on the last move.
        return;
      }

      var turnColor = this.Game.TurnPlayerColor();

      switch (this.FirstClick)
      {
        case null when !cell.OccupiedBy(turnColor):
          throw new Exception($"It's {turnColor}'s turn.");
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
            this.CurrentMoveIndex++;
            this.FirstClick = null;
          }

          break;
        }
      }
    }

    public ISet<Cell> HighlightedCells(bool showPossibleMoves)
    {
      var set = new HashSet<Cell>();

      if (this.FirstClick == null)
      {
        if (this.Game.Moves.Any())
        {
          var lastMove = this.Game.Moves[this.CurrentMoveIndex];

          set.Add(lastMove.FromCell);
          set.Add(lastMove.ToCell);
        }

        return set;
      }

      set.Add(this.FirstClick);

      if (showPossibleMoves)
      {
        set.UnionWith(this.Game.ValidToCells(this.FirstClick));
      }

      return set;
    }

    public void UndoFirstClick()
    {
      this.FirstClick = null;
    }
  }
}