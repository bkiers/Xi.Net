namespace Xi.Models.Extensions
{
  using Xi.Models.Game;

  public static class PieceExtensions
  {
    public static string? SvgName(this Piece? piece)
    {
      return piece == null
        ? null
        : $"{piece.AsciiNotation.ToLower()}{(piece.Color == Color.Red ? "r" : "b")}";
    }
  }
}