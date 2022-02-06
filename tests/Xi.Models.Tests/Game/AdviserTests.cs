namespace Xi.Models.Tests.Game;

using NUnit.Framework;
using Xi.Models.Extensions;
using Xi.Models.Game;

public class AdviserTests
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
                           "8 | . . . . A . . . . " +
                           "9 | . . . . . . . . . ").ToFen());

    var redAdviserCell = board.FindCell<Adviser>(Color.Red);
    var attacking = redAdviserCell.Piece!.Attacking(redAdviserCell, board);

    Assert.AreEqual(4, attacking.Count);
  }

  [Test]
  public void Attacking_CornerOfCastle_ReturnsOneCell()
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
                           "9 | . . . A . . . . . ").ToFen());

    var redAdviserCell = board.FindCell<Adviser>(Color.Red);
    var attacking = redAdviserCell.Piece!.Attacking(redAdviserCell, board);

    Assert.AreEqual(1, attacking.Count);
  }

  [Test]
  public void Attacking_CornerOfCastleKingInCenter_ReturnsNoCell()
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
                           "9 | . . . A . . . . . ").ToFen());

    var redAdviserCell = board.FindCell<Adviser>(Color.Red);
    var attacking = redAdviserCell.Piece!.Attacking(redAdviserCell, board);

    Assert.AreEqual(0, attacking.Count);
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
                           "7 | . . . H . H . . . " +
                           "8 | . . . . A . . . . " +
                           "9 | . . . R . R . . . ").ToFen());

    var redAdviserCell = board.FindCell<Adviser>(Color.Red);
    var attacking = redAdviserCell.Piece!.Attacking(redAdviserCell, board);

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
                           "7 | . . . h . h . . . " +
                           "8 | . . . . A . . . . " +
                           "9 | . . . r . r . . . ").ToFen());

    var redAdviserCell = board.FindCell<Adviser>(Color.Red);
    var attacking = redAdviserCell.Piece!.Attacking(redAdviserCell, board);

    Assert.AreEqual(4, attacking.Count);
  }
}