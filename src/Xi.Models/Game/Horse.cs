namespace Xi.Models.Game
{
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
      if (board.Cell(current, Compass.N)?.Unoccupied == true)
      {
        cells.Add(board.Cell(current, Compass.N, Compass.NW));
        cells.Add(board.Cell(current, Compass.N, Compass.NE));
      }

      if (board.Cell(current, Compass.E)?.Unoccupied == true)
      {
        cells.Add(board.Cell(current, Compass.E, Compass.NE));
        cells.Add(board.Cell(current, Compass.E, Compass.SE));
      }

      if (board.Cell(current, Compass.S)?.Unoccupied == true)
      {
        cells.Add(board.Cell(current, Compass.S, Compass.SW));
        cells.Add(board.Cell(current, Compass.S, Compass.SE));
      }

      if (board.Cell(current, Compass.W)?.Unoccupied == true)
      {
        cells.Add(board.Cell(current, Compass.W, Compass.NW));
        cells.Add(board.Cell(current, Compass.W, Compass.SW));
      }

      // Skip all null-cells and the ones that are occupied by the same color.
      return cells.Where(c => c != null && !c.OccupiedBy(turnColor)).ToList()!;
    }
  }
}