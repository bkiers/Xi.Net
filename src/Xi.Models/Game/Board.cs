namespace Xi.Models.Game
{
  using System;
  using System.Collections.Generic;
  using System.Linq;
  using Xi.Models.Exceptions;
  using Xi.Models.Extensions;

  public class Board : ICloneable
  {
    public const string StartFenNotation = "rheakaehr/9/1c5c1/p1p1p1p1p/9/9/P1P1P1P1P/1C5C1/9/RHEAKAEHR";

    private readonly List<List<Cell>> cells;

    public Board(string fenNotation = StartFenNotation)
    {
      this.cells = fenNotation.ParseFen();
    }

    private Board(List<List<Cell>> cells)
    {
      this.cells = cells;
    }

    public bool KingsEyeingEachOther()
    {
      var redKingCell = this.FindCell<King>(Color.Red);
      var blackKingCell = this.FindCell<King>(Color.Black);

      if (redKingCell.FileIndex != blackKingCell.FileIndex)
      {
        return false;
      }

      for (var r = blackKingCell.RankIndex + 1; r < redKingCell.RankIndex; r++)
      {
        if (this.Cell(redKingCell.FileIndex, r).Occupied)
        {
          return false;
        }
      }

      return true;
    }

    public bool IsCheck(Color color)
    {
      var kingCell = this.FindCell<King>(color);
      var enemyAttacking = this.FindAttackingCellsBy(color.Opposite());

      return enemyAttacking.Contains(kingCell);
    }

    public bool IsCheckmate(Color color)
    {
      return this.IsCheck(color) && this.AllValidToCells(color).Count == 0;
    }

    public Cell Cell(int fileIndex, int rankIndex)
    {
      if (fileIndex is < 0 or > 8)
      {
        throw new ArgumentException($"Invalid fileIndex: {fileIndex}");
      }

      if (rankIndex is < 0 or > 9)
      {
        throw new ArgumentException($"Invalid rankIndex: {rankIndex}");
      }

      return this.cells[rankIndex][fileIndex];
    }

    /// <summary>
    /// Retrieves a cell from a <c>origin</c> towards one or more <c>compasses</c>.
    /// </summary>
    /// <param name="origin">The cell from which to start.</param>
    /// <param name="compasses">The directions to take from the <c>origin</c>.</param>
    /// <returns>
    /// A cell from a <c>origin</c> towards one or more <c>compasses</c>, or<c>null</c> if
    /// no such cell  exists.
    /// </returns>
    /// <exception cref="ArgumentException">If <c>origin</c> was not occupied.</exception>
    public Cell? Cell(Cell origin, params Compass[] compasses)
    {
      if (origin.Unoccupied)
      {
        throw new ArgumentException("The origin must be an occupied cell.");
      }

      var currentCell = origin;

      foreach (var compass in compasses)
      {
        var (deltaFile, deltaRank) = compass.DeltaFileRank(origin.Piece!);

        var newFileIndex = currentCell.FileIndex + deltaFile;
        var newRankIndex = currentCell.RankIndex + deltaRank;

        if (newFileIndex is < 0 or > 8 || newRankIndex is < 0 or > 9)
        {
          return null;
        }

        currentCell = this.Cell(newFileIndex, newRankIndex);
      }

      return currentCell;
    }

    public object Clone()
    {
      var clonedCells = this.cells.Select(r => r.Select(c => (Cell)c.Clone()).ToList()).ToList();

      return new Board(clonedCells);
    }

    public ISet<Cell> FindOccupiedCellsBy(Color color)
    {
      return this.cells.SelectMany(c => c).Where(c => c.OccupiedBy(color)).ToHashSet();
    }

    public ISet<Cell> FindAttackingCellsBy(Color color)
    {
      var occupied = this.FindOccupiedCellsBy(color);
      var attacking = new HashSet<Cell>();

      foreach (var cell in occupied)
      {
        attacking.UnionWith(cell.Piece!.Attacking(cell, this));
      }

      return attacking;
    }


    public ISet<Cell> AllValidToCells(Color color)
    {
      var validCells = new HashSet<Cell>();
      var occupied = this.FindOccupiedCellsBy(color);

      foreach (var fromCell in occupied)
      {
        validCells.UnionWith(this.ValidToCells(fromCell));
      }

      return validCells;
    }

    public ISet<Cell> ValidToCells(Cell fromCell)
    {
      var validCells = new HashSet<Cell>();

      if (fromCell.Unoccupied)
      {
        return validCells;
      }

      var attacking = fromCell.Piece!.Attacking(fromCell, this);

      foreach (var toCell in attacking)
      {
        try
        {
          // Move(...) will throw an exception when it's invalid
          this.Move(fromCell, toCell);
          validCells.Add(toCell);
        }
        catch
        {
          // Ignore this
        }
      }

      return validCells;
    }


    public Cell FindCell<TP>(Color color)
      where TP : Piece
    {
      var cell = this.cells.SelectMany(c => c).FirstOrDefault(c => c.OccupiedBy<TP>(color));

      if (cell == null)
      {
        throw new ArgumentException($"Could not find a {color} {typeof(TP)} on the board.");
      }

      return cell;
    }

    public MoveResult Move(Cell fromCell, Cell toCell)
    {
      return this.Move(fromCell.FileIndex, fromCell.RankIndex, toCell.FileIndex, toCell.RankIndex);
    }

    public MoveResult Move(int fromFileIndex, int fromRankIndex, int toFileIndex, int toRankIndex)
    {
      // Make a copy of the current board: we keep the board immutable!
      var board = (Board)this.Clone();
      var fromCell = board.Cell(fromFileIndex, fromRankIndex);
      var toCell = board.Cell(toFileIndex, toRankIndex);

      if (fromCell.Unoccupied)
      {
        throw new InvalidMoveException($"{fromCell} is not occupied.");
      }

      var colorTurn = fromCell.Piece!.Color;
      var wasCheck = board.IsCheck(colorTurn);

      if (toCell.OccupiedBy(colorTurn))
      {
        throw new InvalidMoveException($"{toCell} is also occupied by {toCell.Piece!.Color}.");
      }

      var attacking = fromCell.Piece!.Attacking(fromCell, this);

      if (!attacking.Contains(toCell))
      {
        throw new InvalidMoveException($"The {colorTurn} {fromCell.Piece!.GetType().Name} cannot move there.");
      }

      var capturedPiece = MovePiece(fromCell, toCell);

      if (board.KingsEyeingEachOther())
      {
        throw new InvalidMoveException("The kings cannot eye each other.");
      }

      if (board.IsCheck(colorTurn))
      {
        throw new InvalidMoveException(wasCheck ? $"{colorTurn} is still check." : $"{colorTurn} cannot self-check.");
      }

      return new MoveResult(board, capturedPiece);
    }

    public IReadOnlyList<Cell> Rank(int rankIndex)
    {
      if (rankIndex is < 0 or > 9)
      {
        throw new ArgumentException($"Invalid rankIndex: {rankIndex}");
      }

      return this.cells[rankIndex].AsReadOnly();
    }

    private static Piece? MovePiece(Cell from, Cell to)
    {
      var capturedPiece = to.Piece;

      to.Piece = from.Piece;
      from.Piece = null;

      return capturedPiece;
    }
  }
}