namespace Xi.Models.Tests.Game
{
  using NUnit.Framework;
  using Xi.Models.Extensions;
  using Xi.Models.Game;

  public class ElephantTests
  {
    [Test]
    public void Attacking_OnOwnSideInCenter_ReturnsFourCells()
    {
      var board = new Board((string.Empty +
        "  | 0 1 2 3 4 5 6 7 8 " +
        "--+------------------- " +
        "0 | . . . . . . . . . " +
        "1 | . . . . . . . . . " +
        "2 | . . . . . . . . . " +
        "3 | . . . . . . . . . " +
        "4 | . . . . . . . . . " +
        "5 | . . . . . . . . . " +
        "6 | . . . . . . . . . " +
        "7 | . . . . E . . . . " +
        "8 | . . . . . . . . . " +
        "9 | . . . . . . . . . ").ToFen());

      var cell = board.FindCell<Elephant>(Color.Red);
      var attacking = cell.Piece!.Attacking(cell, board);

      Assert.AreEqual(4, attacking.Count);
    }

    [Test]
    public void Attacking_OnOwnSideNearTheRiver_ReturnsTwoCells()
    {
      var board = new Board((string.Empty +
        "  | 0 1 2 3 4 5 6 7 8 " +
        "--+------------------- " +
        "0 | . . . . . . . . . " +
        "1 | . . . . . . . . . " +
        "2 | . . . . . . . . . " +
        "3 | . . . . . . . . . " +
        "4 | . . . . . . . . . " +
        "5 | . . E . . . . . . " +
        "6 | . . . . . . . . . " +
        "7 | . . . . . . . . . " +
        "8 | . . . . . . . . . " +
        "9 | . . . . . . . . . ").ToFen());

      var cell = board.FindCell<Elephant>(Color.Red);
      var attacking = cell.Piece!.Attacking(cell, board);

      Assert.AreEqual(2, attacking.Count);
    }

    [Test]
    public void Attacking_OnOwnSideInCenterBlockedByOwnPieces_ReturnsNoCells()
    {
      var board = new Board((string.Empty +
        "  | 0 1 2 3 4 5 6 7 8 " +
        "--+------------------- " +
        "0 | . . . . . . . . . " +
        "1 | . . . . . . . . . " +
        "2 | . . . . . . . . . " +
        "3 | . . . . . . . . . " +
        "4 | . . . . . . . . . " +
        "5 | . . . . . . . . . " +
        "6 | . . . H . H . . . " +
        "7 | . . . . E . . . . " +
        "8 | . . . R . R . . . " +
        "9 | . . . . . . . . . ").ToFen());

      var cell = board.FindCell<Elephant>(Color.Red);
      var attacking = cell.Piece!.Attacking(cell, board);

      Assert.AreEqual(0, attacking.Count);
    }

    [Test]
    public void Attacking_OnOwnSideInCenterBlockedByEnemyPieces_ReturnsNoCells()
    {
      var board = new Board((string.Empty +
        "  | 0 1 2 3 4 5 6 7 8 " +
        "--+------------------- " +
        "0 | . . . . . . . . . " +
        "1 | . . . . . . . . . " +
        "2 | . . . . . . . . . " +
        "3 | . . . . . . . . . " +
        "4 | . . . . . . . . . " +
        "5 | . . . . . . . . . " +
        "6 | . . . h . h . . . " +
        "7 | . . . . E . . . . " +
        "8 | . . . r . r . . . " +
        "9 | . . . . . . . . . ").ToFen());

      var cell = board.FindCell<Elephant>(Color.Red);
      var attacking = cell.Piece!.Attacking(cell, board);

      Assert.AreEqual(0, attacking.Count);
    }

    [Test]
    public void Attacking_OnOwnSideInCenterSurroundedByOwnPieces_ReturnsNoCells()
    {
      var board = new Board((string.Empty +
        "  | 0 1 2 3 4 5 6 7 8 " +
        "--+------------------- " +
        "0 | . . . . . . . . . " +
        "1 | . . . . . . . . . " +
        "2 | . . . . . . . . . " +
        "3 | . . . . . . . . . " +
        "4 | . . . . . . . . . " +
        "5 | . . H . . . H . . " +
        "6 | . . . . . . . . . " +
        "7 | . . . . E . . . . " +
        "8 | . . . . . . . . . " +
        "9 | . . R . . . R . . ").ToFen());

      var cell = board.FindCell<Elephant>(Color.Red);
      var attacking = cell.Piece!.Attacking(cell, board);

      Assert.AreEqual(0, attacking.Count);
    }

    [Test]
    public void Attacking_OnOwnSideInCenterSurroundedByEnemyPieces_ReturnsFourCells()
    {
      var board = new Board((string.Empty +
        "  | 0 1 2 3 4 5 6 7 8 " +
        "--+------------------- " +
        "0 | . . . . . . . . . " +
        "1 | . . . . . . . . . " +
        "2 | . . . . . . . . . " +
        "3 | . . . . . . . . . " +
        "4 | . . . . . . . . . " +
        "5 | . . h . . . h . . " +
        "6 | . . . . . . . . . " +
        "7 | . . . . E . . . . " +
        "8 | . . . . . . . . . " +
        "9 | . . r . . . r . . ").ToFen());

      var cell = board.FindCell<Elephant>(Color.Red);
      var attacking = cell.Piece!.Attacking(cell, board);

      Assert.AreEqual(4, attacking.Count);
    }
  }
}