namespace Xi.Models.Tests.Game
{
  using NUnit.Framework;
  using Xi.Models.Extensions;
  using Xi.Models.Game;

  public class HorseTests
  {
    [Test]
    public void Attacking_InCenter_ReturnsEightCells()
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
        "7 | . . . . H . . . . " +
        "8 | . . . . . . . . . " +
        "9 | . . . . . . . . . ").ToFen());

      var cell = board.FindCell<Horse>(Color.Red);
      var attacking = cell.Piece!.Attacking(cell, board);

      Assert.AreEqual(8, attacking.Count);
    }

    [Test]
    public void Attacking_InCorner_ReturnsTwoCells()
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
        "7 | . . . . . . . . . " +
        "8 | . . . . . . . . . " +
        "9 | H . . . . . . . . ").ToFen());

      var cell = board.FindCell<Horse>(Color.Red);
      var attacking = cell.Piece!.Attacking(cell, board);

      Assert.AreEqual(2, attacking.Count);
    }

    [Test]
    public void Attacking_InCenterBlockedByOwnPieces_ReturnsNoCells()
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
        "6 | . . . . R . . . . " +
        "7 | . . . R H C . . . " +
        "8 | . . . . C . . . . " +
        "9 | . . . . . . . . . ").ToFen());

      var cell = board.FindCell<Horse>(Color.Red);
      var attacking = cell.Piece!.Attacking(cell, board);

      Assert.AreEqual(0, attacking.Count);
    }

    [Test]
    public void Attacking_InCenterBlockedByEnemyPieces_ReturnsNoCells()
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
        "6 | . . . . r . . . . " +
        "7 | . . . r H c . . . " +
        "8 | . . . . c . . . . " +
        "9 | . . . . . . . . . ").ToFen());

      var cell = board.FindCell<Horse>(Color.Red);
      var attacking = cell.Piece!.Attacking(cell, board);

      Assert.AreEqual(0, attacking.Count);
    }

    [Test]
    public void Attacking_InCenterSurroundedByOwnPieces_ReturnsNoCells()
    {
      var board = new Board((string.Empty +
        "  | 0 1 2 3 4 5 6 7 8 " +
        "--+------------------- " +
        "0 | . . . . . . . . . " +
        "1 | . . . . . . . . . " +
        "2 | . . . . . . . . . " +
        "3 | . . . . . . . . . " +
        "4 | . . . . . . . . . " +
        "5 | . . . P . P . . . " +
        "6 | . . P . . . P . . " +
        "7 | . . . . H . . . . " +
        "8 | . . P . . . P . . " +
        "9 | . . . P . P . . . ").ToFen());

      var cell = board.FindCell<Horse>(Color.Red);
      var attacking = cell.Piece!.Attacking(cell, board);

      Assert.AreEqual(0, attacking.Count);
    }

    [Test]
    public void Attacking_InCenterSurroundedByEnemyPieces_ReturnsEightCells()
    {
      var board = new Board((string.Empty +
        "  | 0 1 2 3 4 5 6 7 8 " +
        "--+------------------- " +
        "0 | . . . . . . . . . " +
        "1 | . . . . . . . . . " +
        "2 | . . . . . . . . . " +
        "3 | . . . . . . . . . " +
        "4 | . . . . . . . . . " +
        "5 | . . . p . p . . . " +
        "6 | . . p . . . p . . " +
        "7 | . . . . H . . . . " +
        "8 | . . p . . . p . . " +
        "9 | . . . p . p . . . ").ToFen());

      var cell = board.FindCell<Horse>(Color.Red);
      var attacking = cell.Piece!.Attacking(cell, board);

      Assert.AreEqual(8, attacking.Count);
    }
  }
}