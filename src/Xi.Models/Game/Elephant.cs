namespace Xi.Models.Game;

using System.Collections.Generic;
using System.Linq;

public class Elephant : Piece
{
  public const string AsciiChar = "e";

  public Elephant(Color color)
    : base(AsciiChar, color)
  {
  }

  public override List<Cell> Attacking(Cell current, Board board)
  {
    var turnColor = current.Piece!.Color;
    var cells = new List<Cell?>();

    // An elephant can move 2 steps diagonally in all directions.
    Fill(cells, current, board, Compass.NE);
    Fill(cells, current, board, Compass.SE);
    Fill(cells, current, board, Compass.SW);
    Fill(cells, current, board, Compass.NW);

    // Skip all null-cells, the ones that are occupied by the same color and those on the enemy's side.
    return cells.Where(c => c != null && !c.OccupiedBy(turnColor) && c.OnOwnTerritory(turnColor)).ToList()!;
  }

  private static void Fill(List<Cell?> cells, Cell current, Board board, Compass compass)
  {
    if (board.Cell(current, compass)?.Unoccupied == true)
    {
      cells.Add(board.Cell(current, compass, compass));
    }
  }
}