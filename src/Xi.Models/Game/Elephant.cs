namespace Xi.Models.Game
{
  using System.Collections.Generic;

  public class Elephant : Piece
  {
    public const string AsciiChar = "e";

    public Elephant(Color color)
      : base(AsciiChar, color)
    {
    }

    public override List<Cell> Attacking(Cell current, Board board)
    {
      throw new System.NotImplementedException();
    }
  }
}