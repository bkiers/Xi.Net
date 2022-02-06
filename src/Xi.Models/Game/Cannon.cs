namespace Xi.Models.Game;

using System.Collections.Generic;
using System.Linq;

public class Cannon : Piece
{
  public const string AsciiChar = "c";

  public Cannon(Color color)
    : base(AsciiChar, color)
  {
  }

  public override List<Cell> Attacking(Cell current, Board board)
  {
    var turnColor = current.Piece!.Color;
    var cells = new List<Cell?>();

    // A cannon can move in straight lines and can jump over one piece.
    Fill(cells, current, board, Compass.N, 9);
    Fill(cells, current, board, Compass.E, 8);
    Fill(cells, current, board, Compass.S, 9);
    Fill(cells, current, board, Compass.W, 8);

    // Skip all null-cells and the ones that are occupied by the same color.
    return cells.Where(c => c != null && !c.OccupiedBy(turnColor)).ToList()!;
  }

  private static void Fill(List<Cell?> cells, Cell current, Board board, Compass compass, int maxSteps)
  {
    var hasJumped = false;

    for (var i = 1; i <= maxSteps; i++)
    {
      var cell = board.Cell(current, Enumerable.Repeat(compass, i).ToArray());

      if (cell?.Occupied == true)
      {
        if (hasJumped)
        {
          // We've already jumped: add this cell and return.
          cells.Add(cell);
          return;
        }

        // We've not jumped over a piece yet, now we did (don't add the cell we're jumping over!).
        hasJumped = true;
      }
      else if (!hasJumped)
      {
        cells.Add(cell);
      }
    }
  }
}