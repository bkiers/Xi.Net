namespace Xi.Models.Game
{
  public class Move
  {
    public Move(Cell fromCell, Cell toCell)
    {
      this.FromCell = fromCell;
      this.ToCell = toCell;
    }

    public Cell FromCell { get; }

    public Cell ToCell { get; }
  }
}