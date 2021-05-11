namespace Xi.Models.Tests.Game
{
  using NUnit.Framework;
  using Xi.Models.Extensions;
  using Xi.Models.Game;

  public class KingTests
  {
    [Test]
    public void Attacking_MiddleOfCastle_ReturnsFourCells()
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
        "8 | . . . . K . . . . " +
        "9 | . . . . . . . . . ").ToFen());

      var redKingCell = board.FindCell<King>(Color.Red);
      var attacking = redKingCell.Piece!.Attacking(redKingCell, board);

      Assert.AreEqual(4, attacking.Count);
    }

    [Test]
    public void Attacking_CornerOfCastle_ReturnsTwoCells()
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
        "9 | . . . K . . . . . ").ToFen());

      var redKingCell = board.FindCell<King>(Color.Red);
      var attacking = redKingCell.Piece!.Attacking(redKingCell, board);

      Assert.AreEqual(2, attacking.Count);
    }

    [Test]
    public void Attacking_MiddleOfCastleSurroundedByOwnPieces_ReturnsNoCells()
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
        "8 | . . . H K R . . . " +
        "9 | . . . . R . . . . ").ToFen());

      var redKingCell = board.FindCell<King>(Color.Red);
      var attacking = redKingCell.Piece!.Attacking(redKingCell, board);

      Assert.AreEqual(0, attacking.Count);
    }

    [Test]
    public void Attacking_MiddleOfCastleSurroundedByEnemyPieces_ReturnsFourCells()
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
        "7 | . . . . p . . . . " +
        "8 | . . . h K h . . . " +
        "9 | . . . . p . . . . ").ToFen());

      var redKingCell = board.FindCell<King>(Color.Red);
      var attacking = redKingCell.Piece!.Attacking(redKingCell, board);

      Assert.AreEqual(4, attacking.Count);
    }
  }
}