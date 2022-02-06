namespace Xi.Models.Game;

using System;
using Xi.Models.Extensions;

public class Cell : ICloneable
{
  public Cell(int fileIndex, int rankIndex, Piece? piece = null)
  {
    this.FileIndex = fileIndex;
    this.RankIndex = rankIndex;
    this.Piece = piece;
  }

  public int FileIndex { get; }

  public int RankIndex { get; }

  public Piece? Piece { get; internal set; }

  public bool Occupied => this.Piece != null;

  public bool Unoccupied => this.Piece == null;

  public bool OccupiedBy(Color color) => this.Occupied && this.Piece!.Color == color;

  public bool OccupiedBy<TP>(Color color)
    where TP : Piece
    => this.Occupied && this.Piece!.Color == color && this.Piece.GetType() == typeof(TP);

  public object Clone()
  {
    return new Cell(this.FileIndex, this.RankIndex, this.Piece);
  }

  public override bool Equals(object? obj)
  {
    if (obj == null || this.GetType() != obj.GetType())
    {
      return false;
    }

    var that = (Cell)obj;

    return this.FileIndex == that.FileIndex && this.RankIndex == that.RankIndex;
  }

  public override int GetHashCode()
  {
    return HashCode.Combine(this.FileIndex, this.RankIndex);
  }

  public bool OnEnemyTerritory(Color color)
  {
    return !this.OnOwnTerritory(color);
  }

  public bool OnOwnTerritory(Color color)
  {
    return color.IsBlack() ? this.RankIndex <= 4 : this.RankIndex >= 5;
  }

  public bool InOwnCastle(Color color)
  {
    if (this.FileIndex is < 3 or > 5)
    {
      return false;
    }

    return color.IsBlack() ? this.RankIndex <= 2 : this.RankIndex >= 7;
  }

  public override string ToString()
  {
    return $"(fileIndex: {this.FileIndex}, rankIndex: {this.RankIndex})";
  }
}