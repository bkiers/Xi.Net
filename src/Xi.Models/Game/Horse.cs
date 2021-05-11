namespace Xi.Models.Game
{
  using System.Collections.Generic;

  public class Horse : Piece
  {
    public const string AsciiChar = "h";

    public Horse(Color color)
      : base(AsciiChar, color)
    {
    }

    public override List<Cell> Attacking(Cell current, Board board)
    {
      throw new System.NotImplementedException();
    }
  }
}