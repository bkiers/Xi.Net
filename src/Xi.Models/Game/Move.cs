namespace Xi.Models.Game
{
  using System;

  public class Move
  {
    public Move(Cell fromCell, Cell toCell, string? capturedPiece = null, DateTime? createdAt = null)
    {
      this.FromCell = fromCell;
      this.ToCell = toCell;
      this.CapturedPiece = capturedPiece;
      this.CreatedAt = createdAt;
    }

    public Cell FromCell { get; }

    public Cell ToCell { get; }

    public string? CapturedPiece { get; }

    public DateTime? CreatedAt { get; }

    public string DisplayFor(Color color)
    {
      var formerRank = color == Color.Black ? this.FromCell.RankIndex + 1 : 10 - this.FromCell.RankIndex;
      var formerFile = color == Color.Black ? this.FromCell.FileIndex + 1 : 9 - this.FromCell.FileIndex;
      var newRank = color == Color.Black ? this.ToCell.RankIndex + 1 : 10 - this.ToCell.RankIndex;
      var newFile = color == Color.Black ? this.ToCell.FileIndex + 1 : 9 - this.ToCell.FileIndex;

      return $"({formerRank}{formerFile})-{newRank}{newFile}";
    }

    public override string ToString()
    {
      return $"{this.FromCell} -> {this.ToCell}";
    }
  }
}