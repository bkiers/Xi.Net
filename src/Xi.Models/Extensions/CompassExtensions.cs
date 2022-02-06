namespace Xi.Models.Extensions;

using System;
using Xi.Models.Game;

public static class CompassExtensions
{
  public static (int DeltaFile, int DeltaRank) DeltaFileRank(this Compass compass, Piece piece)
  {
    return (compass.DeltaFile(piece), compass.DeltaRank(piece));
  }

  private static int DeltaFile(this Compass compass, Piece piece)
  {
    switch (compass)
    {
      case Compass.NE:
      case Compass.E:
      case Compass.SE:
        return piece.Color.IsRed() ? 1 : -1;
      case Compass.NW:
      case Compass.W:
      case Compass.SW:
        return piece.Color.IsRed() ? -1 : 1;
      case Compass.N:
      case Compass.S:
        return 0;
      default:
        throw new ArgumentException($"Unknown direction: {compass}");
    }
  }

  private static int DeltaRank(this Compass compass, Piece piece)
  {
    switch (compass)
    {
      case Compass.NW:
      case Compass.N:
      case Compass.NE:
        return piece.Color.IsRed() ? -1 : 1;
      case Compass.SW:
      case Compass.S:
      case Compass.SE:
        return piece.Color.IsRed() ? 1 : -1;
      case Compass.W:
      case Compass.E:
        return 0;
      default:
        throw new ArgumentException($"Unknown direction: {compass}");
    }
  }
}