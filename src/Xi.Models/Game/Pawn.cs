namespace Xi.Models.Game;

using System.Collections.Generic;
using System.Linq;

public class Pawn : Piece
{
  public const string AsciiChar = "p";

  public Pawn(Color color)
    : base(AsciiChar, color)
  {
  }

  public override List<Cell> Attacking(Cell current, Board board)
  {
    var turnColor = current.Piece!.Color;

    // A pawn can move 1 step ahead.
    var cells = new List<Cell?> { board.Cell(current, Compass.N) };

    if (current.OnEnemyTerritory(turnColor))
    {
      // If the pawn is on enemy territory, it can also move left and right.
      cells.Add(board.Cell(current, Compass.W));
      cells.Add(board.Cell(current, Compass.E));
    }

    // Skip all null-cells and the ones that are occupied by the same color.
    return cells.Where(c => c != null && !c.OccupiedBy(turnColor)).ToList()!;
  }
}