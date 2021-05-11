namespace Xi.Models.Game
{
  using System.Collections.Generic;
  using System.Linq;

  public class Rook : Piece
  {
    public const string AsciiChar = "r";

    public Rook(Color color)
      : base(AsciiChar, color)
    {
    }

    public override List<Cell> Attacking(Cell current, Board board)
    {
      var turnColor = current.Piece!.Color;
      var cells = new List<Cell?>();

      // A rook can move in straight lines.
      Fill(cells, current, board, Compass.N, 9);
      Fill(cells, current, board, Compass.E, 8);
      Fill(cells, current, board, Compass.S, 9);
      Fill(cells, current, board, Compass.W, 8);

      // Skip all null-cells and the ones that are occupied by the same color.
      return cells.Where(c => c != null && !c.OccupiedBy(turnColor)).ToList()!;
    }


    private static void Fill(List<Cell?> cells, Cell current, Board board, Compass compass, int maxSteps)
    {
      for (var i = 1; i <= maxSteps; i++)
      {
        cells.Add(board.Cell(current, Enumerable.Repeat(compass, i).ToArray()));

        if (cells.Last()?.Occupied == true)
        {
          // Stop walking in the direction when we encounter an occupied cell: a rook cannot jump.
          break;
        }
      }
    }
  }
}