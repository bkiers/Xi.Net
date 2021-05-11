namespace Xi.Models.Tests.Game
{
  using NUnit.Framework;
  using Xi.Models.Extensions;
  using Xi.Models.Game;

  public class CannonTests
  {
    [Test]
    public void Attacking_InCenter_ReturnsSeventeenCells()
    {
      var board = new Board((string.Empty +
        "  | 0 1 2 3 4 5 6 7 8 " +
        "--+------------------- " +
        "0 | . . . . . . . . . " +
        "1 | . . . . . . . . . " +
        "2 | . . . . . . . . . " +
        "3 | . . . . . . . . . " +
        "4 | . . . . . . . . . " +
        "5 | . . . . C . . . . " +
        "6 | . . . . . . . . . " +
        "7 | . . . . . . . . . " +
        "8 | . . . . . . . . . " +
        "9 | . . . . . . . . . ").ToFen());

      var cell = board.FindCell<Cannon>(Color.Red);
      var attacking = cell.Piece!.Attacking(cell, board);

      Assert.AreEqual(17, attacking.Count);
    }

    [Test]
    public void Attacking_InCorner_ReturnsSeventeenCells()
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
        "9 | C . . . . . . . . ").ToFen());

      var cell = board.FindCell<Cannon>(Color.Red);
      var attacking = cell.Piece!.Attacking(cell, board);

      Assert.AreEqual(17, attacking.Count);
    }

    [Test]
    public void Attacking_InCornerBlockedByPieces_ReturnsNoCells()
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
        "8 | r . . . . . . . . " +
        "9 | C R . . . . . . . ").ToFen());

      var cell = board.FindCell<Cannon>(Color.Red);
      var attacking = cell.Piece!.Attacking(cell, board);

      Assert.AreEqual(0, attacking.Count);
    }

    [Test]
    public void Attacking_InCornerBlockedByPiecesEnemyTargets_ReturnsTwoCells()
    {
      var board = new Board((string.Empty +
        "  | 0 1 2 3 4 5 6 7 8 " +
        "--+------------------- " +
        "0 | p . . . . . . . . " +
        "1 | . . . . . . . . . " +
        "2 | . . . . . . . . . " +
        "3 | . . . . . . . . . " +
        "4 | . . . . . . . . . " +
        "5 | . . . . . . . . . " +
        "6 | . . . . . . . . . " +
        "7 | . . . . . . . . . " +
        "8 | r . . . . . . . . " +
        "9 | C R . . . . . . p ").ToFen());

      var cell = board.FindCell<Cannon>(Color.Red);
      var attacking = cell.Piece!.Attacking(cell, board);

      Assert.AreEqual(2, attacking.Count);
    }

    [Test]
    public void Attacking_InCornerBlockedByPiecesOwnTargets_ReturnsNoCells()
    {
      var board = new Board((string.Empty +
        "  | 0 1 2 3 4 5 6 7 8 " +
        "--+------------------- " +
        "0 | P . . . . . . . . " +
        "1 | . . . . . . . . . " +
        "2 | . . . . . . . . . " +
        "3 | . . . . . . . . . " +
        "4 | . . . . . . . . . " +
        "5 | . . . . . . . . . " +
        "6 | . . . . . . . . . " +
        "7 | . . . . . . . . . " +
        "8 | r . . . . . . . . " +
        "9 | C R . . . . . . P ").ToFen());

      var cell = board.FindCell<Cannon>(Color.Red);
      var attacking = cell.Piece!.Attacking(cell, board);

      Assert.AreEqual(0, attacking.Count);
    }

    [Test]
    public void Attacking_InCenterEnemyTargets_ReturnsTwelveCells()
    {
      var board = new Board((string.Empty +
        "  | 0 1 2 3 4 5 6 7 8 " +
        "--+------------------- " +
        "0 | . . . . . . . . . " +
        "1 | . . . . p . . . . " +
        "2 | . . . . P . . . . " +
        "3 | . . . . . . . . . " +
        "4 | . . . . . . . . . " +
        "5 | p P . . C . . p p " +
        "6 | . . . . . . . . . " +
        "7 | . . . . . . . . . " +
        "8 | . . . . p . . . . " +
        "9 | . . . . p . . . . ").ToFen());

      var cell = board.FindCell<Cannon>(Color.Red);
      var attacking = cell.Piece!.Attacking(cell, board);

      Assert.AreEqual(12, attacking.Count);
    }
  }
}