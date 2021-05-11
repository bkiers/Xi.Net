namespace Xi.Models.Game
{
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
      if (board.Cell(current, Compass.NE)?.Unoccupied == true)
      {
        cells.Add(board.Cell(current, Compass.NE, Compass.NE));
      }

      if (board.Cell(current, Compass.SE)?.Unoccupied == true)
      {
        cells.Add(board.Cell(current, Compass.SE, Compass.SE));
      }

      if (board.Cell(current, Compass.SW)?.Unoccupied == true)
      {
        cells.Add(board.Cell(current, Compass.SW, Compass.SW));
      }

      if (board.Cell(current, Compass.NW)?.Unoccupied == true)
      {
        cells.Add(board.Cell(current, Compass.NW, Compass.NW));
      }

      // Skip all null-cells, the ones that are occupied by the same color and those on the enemy's side.
      return cells.Where(c => c != null && !c.OccupiedBy(turnColor) && c.OnOwnTerritory(turnColor)).ToList()!;
    }
  }
}