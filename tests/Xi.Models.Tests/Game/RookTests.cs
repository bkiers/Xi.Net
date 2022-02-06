namespace Xi.Models.Tests.Game;

using NUnit.Framework;
using Xi.Models.Extensions;
using Xi.Models.Game;

public class RookTests
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
                           "5 | . . . . R . . . . " +
                           "6 | . . . . . . . . . " +
                           "7 | . . . . . . . . . " +
                           "8 | . . . . . . . . . " +
                           "9 | . . . . . . . . . ").ToFen());

    var cell = board.FindCell<Rook>(Color.Red);
    var attacking = cell.Piece!.Attacking(cell, board);

    Assert.AreEqual(17, attacking.Count);
  }

  [Test]
  public void Attacking_InCenterAlmostSurroundedByOwnPieces_ReturnsFourCells()
  {
    var board = new Board((string.Empty +
                           "  | 0 1 2 3 4 5 6 7 8 " +
                           "--+------------------- " +
                           "0 | . . . . . . . . . " +
                           "1 | . . . . . . . . . " +
                           "2 | . . . . . . . . . " +
                           "3 | . . . . P . . . . " +
                           "4 | . . . . . . . . . " +
                           "5 | . . C . R . H . . " +
                           "6 | . . . . . . . . . " +
                           "7 | . . . . C . . . . " +
                           "8 | . . . . . . . . . " +
                           "9 | . . . . . . . . . ").ToFen());

    var cell = board.FindCell<Rook>(Color.Red);
    var attacking = cell.Piece!.Attacking(cell, board);

    Assert.AreEqual(4, attacking.Count);
  }

  [Test]
  public void Attacking_InCenterAlmostSurroundedByEnemyPieces_ReturnsEightCells()
  {
    var board = new Board((string.Empty +
                           "  | 0 1 2 3 4 5 6 7 8 " +
                           "--+------------------- " +
                           "0 | . . . . . . . . . " +
                           "1 | . . . . . . . . . " +
                           "2 | . . . . . . . . . " +
                           "3 | . . . . p . . . . " +
                           "4 | . . . . . . . . . " +
                           "5 | . . c . R . h . . " +
                           "6 | . . . . . . . . . " +
                           "7 | . . . . c . . . . " +
                           "8 | . . . . . . . . . " +
                           "9 | . . . . . . . . . ").ToFen());

    var cell = board.FindCell<Rook>(Color.Red);
    var attacking = cell.Piece!.Attacking(cell, board);

    Assert.AreEqual(8, attacking.Count);
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
                           "9 | R . . . . . . . . ").ToFen());

    var cell = board.FindCell<Rook>(Color.Red);
    var attacking = cell.Piece!.Attacking(cell, board);

    Assert.AreEqual(17, attacking.Count);
  }

  [Test]
  public void Attacking_InCornerBlockedByOwnPieces_ReturnsNoCells()
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
                           "8 | C . . . . . . . . " +
                           "9 | R C . . . . . . . ").ToFen());

    var cell = board.FindCell<Rook>(Color.Red);
    var attacking = cell.Piece!.Attacking(cell, board);

    Assert.AreEqual(0, attacking.Count);
  }

  [Test]
  public void Attacking_InCornerBlockedByEnemyPieces_ReturnsTwoCells()
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
                           "8 | c . . . . . . . . " +
                           "9 | R c . . . . . . . ").ToFen());

    var cell = board.FindCell<Rook>(Color.Red);
    var attacking = cell.Piece!.Attacking(cell, board);

    Assert.AreEqual(2, attacking.Count);
  }
}