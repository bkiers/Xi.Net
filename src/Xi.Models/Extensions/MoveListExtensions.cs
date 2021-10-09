namespace Xi.Models.Extensions
{
  using System.Collections.Generic;
  using System.Linq;
  using Xi.Models.Game;

  public static class MoveListExtensions
  {
    public static IEnumerable<Move> For(this List<Move> moves, Color color)
    {
      var remainder = color == Color.Red ? 0 : 1;

      return moves.Where((m, i) => i % 2 == remainder);
    }
  }
}