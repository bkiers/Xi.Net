namespace Xi.Models.Game;

using System.Collections.Generic;
using System.Linq;

public class Horse : Piece
{
  public const string AsciiChar = "h";

  public Horse(Color color)
    : base(AsciiChar, color)
  {
  }

  public override List<Cell> Attacking(Cell current, Board board)
  {
    var turnColor = current.Piece!.Color;
    var cells = new List<Cell?>();

    // A horse can move 1 steps straight, and then 1 diagonally either way.
    Fill(cells, current, board, Compass.N, Compass.NW);
    Fill(cells, current, board, Compass.N, Compass.NE);
    Fill(cells, current, board, Compass.E, Compass.NE);
    Fill(cells, current, board, Compass.E, Compass.SE);
    Fill(cells, current, board, Compass.S, Compass.SE);
    Fill(cells, current, board, Compass.S, Compass.SW);
    Fill(cells, current, board, Compass.W, Compass.NW);
    Fill(cells, current, board, Compass.W, Compass.SW);

    // Skip all null-cells and the ones that are occupied by the same color.
    return cells.Where(c => c != null && !c.OccupiedBy(turnColor)).ToList()!;
  }

  private static void Fill(List<Cell?> cells, Cell current, Board board, Compass step1, Compass step2)
  {
    if (board.Cell(current, step1)?.Unoccupied == true)
    {
      cells.Add(board.Cell(current, step1, step2));
    }
  }
}