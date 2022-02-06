namespace Xi.Models.Game;

using System.Collections.Generic;
using System.Linq;

public class King : Piece
{
  public const string AsciiChar = "k";

  public King(Color color)
    : base(AsciiChar, color)
  {
  }

  public override List<Cell> Attacking(Cell current, Board board)
  {
    var turnColor = current.Piece!.Color;

    // The king can move 1 step in all directions.
    var cells = new List<Cell?>
    {
      board.Cell(current, Compass.N),
      board.Cell(current, Compass.E),
      board.Cell(current, Compass.S),
      board.Cell(current, Compass.W),
    };

    // Skip all null-cells, the ones that are occupied by the same color and those outside the king's own castle.
    return cells.Where(c => c != null && !c.OccupiedBy(turnColor) && c.InOwnCastle(turnColor)).ToList()!;
  }
}