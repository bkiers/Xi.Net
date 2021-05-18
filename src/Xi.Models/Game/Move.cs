namespace Xi.Models.Game
{
  using System;

  public class Move
  {
    public Move(Cell fromCell, Cell toCell, DateTime? createdAt = null)
    {
      this.FromCell = fromCell;
      this.ToCell = toCell;
      this.CreatedAt = createdAt ?? DateTime.UtcNow;
    }

    public Cell FromCell { get; }

    public Cell ToCell { get; }

    public DateTime CreatedAt { get; }
  }
}