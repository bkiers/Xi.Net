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

    public Cell Find<TP>(Color color)
      where TP : Piece
    {
      var cell = this.cells.SelectMany(c => c).FirstOrDefault(c => c.OccupiedBy<TP>(color));

      if (cell == null)
      {
        throw new ArgumentException($"Could not find a {color} {typeof(TP)} on the board.");
      }

      return cell;
    }

    public MoveResult Move(Color colorTurn, int fromFileIndex, int fromRankIndex, int toFileIndex, int toRankIndex)
    {
      // Make a copy of the current board: we keep the board immutable!
      var board = (Board)this.Clone();
      var fromCell = board.Cell(fromFileIndex, fromRankIndex);
      var toCell = board.Cell(toFileIndex, toRankIndex);

      if (fromCell.Unoccupied)
      {
        throw new InvalidMoveException($"{fromCell} is not occupied.");
      }

      if (fromCell.Piece!.Color != colorTurn)
      {
        throw new InvalidMoveException($"It's {colorTurn}'s turn.");
      }

      if (toCell.Occupied && toCell.Piece!.Color == fromCell.Piece!.Color)
      {
        throw new InvalidMoveException($"{toCell} is also occupied by {toCell.Piece!.Color}.");
      }

      var capturedPiece = MovePiece(fromCell, toCell);

      // TODO: kings eyeing each other?
      // TODO: check/checkmate?
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