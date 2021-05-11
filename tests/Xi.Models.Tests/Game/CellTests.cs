namespace Xi.Models.Tests.Game
{
  using NUnit.Framework;
  using Xi.Models.Game;

  public class CellTests
  {
    [Test]
    public void OccupiedBy_UnoccupiedCell_ReturnsFalse()
    {
      var board = new Board();

      Assert.False(board.Cell(1, 1).OccupiedBy<Rook>(Color.Black));
    }

    [Test]
    public void OccupiedBy_OccupiedCellWrongPiece_ReturnsFalse()
    {
      var board = new Board();

      Assert.False(board.Cell(0, 0).OccupiedBy<Horse>(Color.Black));
    }

    [Test]
    public void OccupiedBy_OccupiedCellCorrectPieceWrongColorPiece_ReturnsFalse()
    {
      var board = new Board();

      Assert.False(board.Cell(1, 0).OccupiedBy<Horse>(Color.Red));
    }

    [Test]
    public void OccupiedBy_OccupiedCellCorrectPieceCorrectColor_ReturnsTrue()
    {
      var board = new Board();

      Assert.True(board.Cell(1, 0).OccupiedBy<Horse>(Color.Black));
    }
  }
}