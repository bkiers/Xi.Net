namespace Xi.Models.Game
{
  using System.Collections.Generic;
  using System.Linq;

  public class Adviser : Piece
  {
    public const string AsciiChar = "a";

    public Adviser(Color color)
      : base(AsciiChar, color)
    {
    }

    public override List<Cell> Attacking(Cell current, Board board)
    {
      var turnColor = current.Piece!.Color;

      // An adviser can move 1 step diagonally in all directions.
      var cells = new List<Cell?>
      {
        board.Cell(current, Compass.NE),
        board.Cell(current, Compass.SE),
        board.Cell(current, Compass.SW),
        board.Cell(current, Compass.NW),
      };

      // Skip all null-cells, the ones that are occupied by the same color and those outside the adviser's own castle.
      return cells.Where(c => c != null && !c.OccupiedBy(turnColor) && c.InOwnCastle(turnColor)).ToList()!;
    }
  }
}