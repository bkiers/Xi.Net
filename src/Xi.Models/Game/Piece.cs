namespace Xi.Models.Game
{
  using System;
  using System.Collections.Generic;

  public abstract class Piece
  {
    public const string NoPiece = ".";

    private readonly string asciiNotation;

    protected Piece(string asciiNotation, Color color)
    {
      this.asciiNotation = asciiNotation.ToLower();
      this.Color = color;
    }

    public string AsciiNotation => this.Color == Color.Red ? this.asciiNotation.ToUpper() : this.asciiNotation;

    public Color Color { get; }

    public static Piece? Parse(char asciiNotation)
    {
      var color = char.IsLower(asciiNotation) ? Color.Black : Color.Red;

      return asciiNotation.ToString().ToLower() switch
      {
        Pawn.AsciiChar => new Pawn(color),
        Cannon.AsciiChar => new Cannon(color),
        Rook.AsciiChar => new Rook(color),
        Horse.AsciiChar => new Horse(color),
        Elephant.AsciiChar => new Elephant(color),
        Adviser.AsciiChar => new Adviser(color),
        King.AsciiChar => new King(color),
        NoPiece => null,
        _ => throw new ArgumentException($"Unknown piece for ASCII char: {asciiNotation}"),
      };
    }

    public abstract List<Cell> Attacking(Cell current, Board board);

    public override bool Equals(object? obj)
    {
      if (obj == null || this.GetType() != obj.GetType())
      {
        return false;
      }

      Piece that = (Piece)obj;

      return this.AsciiNotation == that.AsciiNotation;
    }

    public override int GetHashCode()
    {
      return this.AsciiNotation.GetHashCode();
    }
  }
}