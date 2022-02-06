namespace Xi.Models.Extensions;

using Xi.Models.Game;

public static class ColorExtensions
{
  public static bool IsRed(this Color color)
  {
    return color == Color.Red;
  }

  public static bool IsBlack(this Color color)
  {
    return color == Color.Black;
  }

  public static Color Opposite(this Color color)
  {
    return color.IsRed() ? Color.Black : Color.Red;
  }
}