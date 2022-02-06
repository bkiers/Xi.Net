namespace Xi.Models.Tests.Game;

using System;
using System.Linq;
using NUnit.Framework;
using Xi.Models.Exceptions;
using Xi.Models.Extensions;
using Xi.Models.Game;

public class BoardTests
{
  [Test]
  public void IsCheckmate_CannonTrap_ReturnsTrue()
  {
    var board = new Board((string.Empty +
                           "  | 0 1 2 3 4 5 6 7 8 " +
                           "--+------------------- " +
                           "0 | . . . . k . . . . " +
                           "1 | . C . . . . . . . " +
                           "2 | . . . . . . . . . " +
                           "3 | . . . . c . . . . " +
                           "4 | . . . . c . . . . " +
                           "5 | . . . P . . . C . " +
                           "6 | . . . . . . . . . " +
                           "7 | . . . . . . . . R " +
                           "8 | R . . . . . . . . " +
                           "9 | . . . A K A . . . ").ToFen());

    Assert.True(board.IsCheckmate(Color.Red));
  }

  [Test]
  public void AllValidToCells_InCheck_ReturnsThreeCell()
  {
    var board = new Board((string.Empty +
                           "  | 0 1 2 3 4 5 6 7 8 " +
                           "--+------------------- " +
                           "0 | . . . . k . . . . " +
                           "1 | . . . . . . . . . " +
                           "2 | . . . . . . . . . " +
                           "3 | . . . . r . . . . " +
                           "4 | . . . . . . . . . " +
                           "5 | . . . . . . . . . " +
                           "6 | . . . . . . . . . " +
                           "7 | . . . . . . . . . " +
                           "8 | R . . . . . . . . " +
                           "9 | . . . . K . . . . ").ToFen());

    Assert.AreEqual(3, board.AllValidToCells(Color.Red).Count);
  }

  [Test]
  public void ValidToCells_InCheck_ReturnsOneCell()
  {
    var board = new Board((string.Empty +
                           "  | 0 1 2 3 4 5 6 7 8 " +
                           "--+------------------- " +
                           "0 | . . . . k . . . . " +
                           "1 | . . . . . . . . . " +
                           "2 | . . . . . . . . . " +
                           "3 | . . . . r . . . . " +
                           "4 | . . . . . . . . . " +
                           "5 | . . . . . . . . . " +
                           "6 | . . . . . . . . . " +
                           "7 | . . . . . . . . . " +
                           "8 | R . . . . . . . . " +
                           "9 | . . . . K . . . . ").ToFen());

    var rookCell = board.FindCell<Rook>(Color.Red);

    Assert.AreEqual(1, board.ValidToCells(rookCell).Count);
  }

  [Test]
  public void ValidToCells_LockedAdviser_ReturnsNoCells()
  {
    var board = new Board((string.Empty +
                           "  | 0 1 2 3 4 5 6 7 8 " +
                           "--+------------------- " +
                           "0 | . . . . k . . . . " +
                           "1 | . . . . . . . . . " +
                           "2 | . . . . . . . . . " +
                           "3 | . . . . r . . . . " +
                           "4 | . . . . . . . . . " +
                           "5 | . . . . . . . . . " +
                           "6 | . . . . . . . . . " +
                           "7 | . . . . . . . . . " +
                           "8 | . . . . A . . . . " +
                           "9 | . . . . K . . . . ").ToFen());

    var adviserCell = board.FindCell<Adviser>(Color.Red);

    Assert.IsEmpty(board.ValidToCells(adviserCell));
  }

  [Test]
  public void ValidToCells_FreeAdviser_ReturnsFourCells()
  {
    var board = new Board((string.Empty +
                           "  | 0 1 2 3 4 5 6 7 8 " +
                           "--+------------------- " +
                           "0 | . . . . k . . . . " +
                           "1 | . . . . . . . . . " +
                           "2 | . . . . . . . . . " +
                           "3 | . . . . r . . . . " +
                           "4 | . . . . . . . . . " +
                           "5 | . . . . . . . . . " +
                           "6 | . . . . . . . . . " +
                           "7 | . . . . . . . . . " +
                           "8 | . . . . A K . . . " +
                           "9 | . . . . . . . . . ").ToFen());

    var adviserCell = board.FindCell<Adviser>(Color.Red);

    Assert.AreEqual(4, board.ValidToCells(adviserCell).Count);
  }

  [Test]
  public void Move_InvalidRookMove_ThrowsInvalidMoveException()
  {
    var board = new Board((string.Empty +
                           "  | 0 1 2 3 4 5 6 7 8 " +
                           "--+------------------- " +
                           "0 | . . . . k . . . . " +
                           "1 | . . . . . . . . . " +
                           "2 | . . . . . . . . . " +
                           "3 | . . . . . . . . . " +
                           "4 | . . . . . . . . . " +
                           "5 | R . . . . . . . . " +
                           "6 | . . . . . . . . . " +
                           "7 | . . . . . . . . . " +
                           "8 | . . . . A . . . . " +
                           "9 | . . . . K . . . . ").ToFen());

    var rookCell = board.FindCell<Rook>(Color.Red);

    var ex = Assert.Throws<InvalidMoveException>(() => board.Move(rookCell, board.Cell(rookCell, Compass.NE)!));

    Assert.True(ex!.Message.Contains("Red Rook cannot move there"));
  }

  [Test]
  public void Move_FromUnOccupied_ThrowsInvalidMoveException()
  {
    var board = new Board((string.Empty +
                           "  | 0 1 2 3 4 5 6 7 8 " +
                           "--+------------------- " +
                           "0 | . . . . k . . . . " +
                           "1 | . . . . . . . . . " +
                           "2 | . . . . . . . . . " +
                           "3 | . . . . . . . . . " +
                           "4 | . . . . . . . . . " +
                           "5 | R . . . . . . . . " +
                           "6 | . . . . . . . . . " +
                           "7 | . . . . . . . . . " +
                           "8 | . . . . A . . . . " +
                           "9 | . . . . K . . . . ").ToFen());

    var ex = Assert.Throws<InvalidMoveException>(() => board.Move(0, 4, 1, 4));

    Assert.True(ex!.Message.Contains("is not occupied"));
  }

  [Test]
  public void Move_ToOccupiedByOwn_ThrowsInvalidMoveException()
  {
    var board = new Board((string.Empty +
                           "  | 0 1 2 3 4 5 6 7 8 " +
                           "--+------------------- " +
                           "0 | . . . . k . . . . " +
                           "1 | . . . . . . . . . " +
                           "2 | . . . . . . . . . " +
                           "3 | . . . . . . . . . " +
                           "4 | . . . . . . . . . " +
                           "5 | R P . . . . . . . " +
                           "6 | . . . . . . . . . " +
                           "7 | . . . . . . . . . " +
                           "8 | . . . . A . . . . " +
                           "9 | . . . . K . . . . ").ToFen());

    var ex = Assert.Throws<InvalidMoveException>(() => board.Move(0, 5, 1, 5));

    Assert.True(ex!.Message.Contains("also occupied by"));
  }

  [Test]
  public void Move_KingsEyeingEachOther_ThrowsInvalidMoveException()
  {
    var board = new Board((string.Empty +
                           "  | 0 1 2 3 4 5 6 7 8 " +
                           "--+------------------- " +
                           "0 | . . . . k . . . . " +
                           "1 | . . . . . . . . . " +
                           "2 | . . . . . . . . . " +
                           "3 | . . . . . . . . . " +
                           "4 | . . . . . . . . . " +
                           "5 | . . . . . . . . . " +
                           "6 | . . . . . . . . . " +
                           "7 | . . . . . . . . . " +
                           "8 | . . . . A . . . . " +
                           "9 | . . . . K . . . . ").ToFen());

    var advisorCell = board.FindCell<Adviser>(Color.Red);

    var ex = Assert.Throws<InvalidMoveException>(() => board.Move(advisorCell, board.Cell(advisorCell, Compass.SW)!));

    Assert.True(ex!.Message.Contains("eye each other"));
  }

  [Test]
  public void Move_SelfCheck_ThrowsInvalidMoveException()
  {
    var board = new Board((string.Empty +
                           "  | 0 1 2 3 4 5 6 7 8 " +
                           "--+------------------- " +
                           "0 | . . . . k . . . . " +
                           "1 | . . . . . . . . . " +
                           "2 | . . . . . . . . . " +
                           "3 | . . . . r . . . . " +
                           "4 | . . . . . . . . . " +
                           "5 | . . . . . . . . . " +
                           "6 | . . . . . . . . . " +
                           "7 | . . . . . . . . . " +
                           "8 | . . . . A . . . . " +
                           "9 | . . . . K . . . . ").ToFen());

    var advisorCell = board.FindCell<Adviser>(Color.Red);

    var ex = Assert.Throws<InvalidMoveException>(() => board.Move(advisorCell, board.Cell(advisorCell, Compass.SW)!));

    Assert.True(ex!.Message.Contains("cannot self-check"));
  }

  [Test]
  public void Move_StillCheck_ThrowsInvalidMoveException()
  {
    var board = new Board((string.Empty +
                           "  | 0 1 2 3 4 5 6 7 8 " +
                           "--+------------------- " +
                           "0 | . . . . k . . . . " +
                           "1 | . . . . . . . . . " +
                           "2 | . . . . . . . . . " +
                           "3 | . . . . r . . . . " +
                           "4 | . . . . . . . . . " +
                           "5 | . . . . . . . . . " +
                           "6 | . . . . . . . . . " +
                           "7 | . . . . . . . . . " +
                           "8 | R . . . . . . . . " +
                           "9 | . . . . K . . . . ").ToFen());

    var rookCell = board.FindCell<Rook>(Color.Red);

    var ex = Assert.Throws<InvalidMoveException>(() => board.Move(rookCell, board.Cell(rookCell, Compass.E)!));

    Assert.True(ex!.Message.Contains("still check"));
  }

  [Test]
  public void KingsEyeingEachOther_AreEyeing_ReturnsTrue()
  {
    var board = new Board((string.Empty +
                           "  | 0 1 2 3 4 5 6 7 8 " +
                           "--+------------------- " +
                           "0 | . . . . . . . . . " +
                           "1 | . . . . . . . . . " +
                           "2 | . . . . k . . . . " +
                           "3 | . . . . . . . . . " +
                           "4 | . . . . . . . . . " +
                           "5 | . . . . . . . . . " +
                           "6 | . . . . . . . . . " +
                           "7 | . . . . K . . . . " +
                           "8 | . . . . . . . . . " +
                           "9 | . . . . . . . . . ").ToFen());

    Assert.True(board.KingsEyeingEachOther());
  }

  [Test]
  public void KingsEyeingEachOther_PieceInBetween_ReturnsFalse()
  {
    var board = new Board((string.Empty +
                           "  | 0 1 2 3 4 5 6 7 8 " +
                           "--+------------------- " +
                           "0 | . . . . . . . . . " +
                           "1 | . . . . . . . . . " +
                           "2 | . . . . k . . . . " +
                           "3 | . . . . . . . . . " +
                           "4 | . . . . . . . . . " +
                           "5 | . . . . . . . . . " +
                           "6 | . . . . P . . . . " +
                           "7 | . . . . K . . . . " +
                           "8 | . . . . . . . . . " +
                           "9 | . . . . . . . . . ").ToFen());

    Assert.False(board.KingsEyeingEachOther());
  }

  [Test]
  public void KingsEyeingEachOther_OtherFile_ReturnsFalse()
  {
    var board = new Board((string.Empty +
                           "  | 0 1 2 3 4 5 6 7 8 " +
                           "--+------------------- " +
                           "0 | . . . . . . . . . " +
                           "1 | . . . . . . . . . " +
                           "2 | . . . . k . . . . " +
                           "3 | . . . . . . . . . " +
                           "4 | . . . . . . . . . " +
                           "5 | . . . . . . . . . " +
                           "6 | . . . . . . . . . " +
                           "7 | . . . K . . . . . " +
                           "8 | . . . . . . . . . " +
                           "9 | . . . . . . . . . ").ToFen());

    Assert.False(board.KingsEyeingEachOther());
  }

  [Test]
  public void IsCheck_PawnInFront_ReturnsTrue()
  {
    var board = new Board((string.Empty +
                           "  | 0 1 2 3 4 5 6 7 8 " +
                           "--+------------------- " +
                           "0 | . . . . k . . . . " +
                           "1 | . . . . P . . . . " +
                           "2 | . . . . . . . . . " +
                           "3 | . . . . . . . . . " +
                           "4 | . . . . . . . . . " +
                           "5 | . . . . . . . . . " +
                           "6 | . . . . . . . . . " +
                           "7 | . . . . . . . . . " +
                           "8 | . . . . . . . . . " +
                           "9 | . . . . . . . . . ").ToFen());

    Assert.True(board.IsCheck(Color.Black));
  }

  [Test]
  public void IsCheck_PawnFromSide_ReturnsTrue()
  {
    var board = new Board((string.Empty +
                           "  | 0 1 2 3 4 5 6 7 8 " +
                           "--+------------------- " +
                           "0 | . . . . k P . . . " +
                           "1 | . . . . . . . . . " +
                           "2 | . . . . . . . . . " +
                           "3 | . . . . . . . . . " +
                           "4 | . . . . . . . . . " +
                           "5 | . . . . . . . . . " +
                           "6 | . . . . . . . . . " +
                           "7 | . . . . . . . . . " +
                           "8 | . . . . . . . . . " +
                           "9 | . . . . . . . . . ").ToFen());

    Assert.True(board.IsCheck(Color.Black));
  }

  [Test]
  public void IsCheck_PawnFromBack_ReturnsFalse()
  {
    var board = new Board((string.Empty +
                           "  | 0 1 2 3 4 5 6 7 8 " +
                           "--+------------------- " +
                           "0 | . . . . . P . . . " +
                           "1 | . . . . . k . . . " +
                           "2 | . . . . . . . . . " +
                           "3 | . . . . . . . . . " +
                           "4 | . . . . . . . . . " +
                           "5 | . . . . . . . . . " +
                           "6 | . . . . . . . . . " +
                           "7 | . . . . . . . . . " +
                           "8 | . . . . . . . . . " +
                           "9 | . . . . . . . . . ").ToFen());

    Assert.False(board.IsCheck(Color.Black));
  }

  [Test]
  public void IsCheck_Cannon_ReturnsTrue()
  {
    var board = new Board((string.Empty +
                           "  | 0 1 2 3 4 5 6 7 8 " +
                           "--+------------------- " +
                           "0 | . . . . k . . . . " +
                           "1 | . . . . . . . . . " +
                           "2 | . . . . . . . . . " +
                           "3 | . . . . p . . . . " +
                           "4 | . . . . . . . . . " +
                           "5 | . . . . . . . . . " +
                           "6 | . . . . C . . . . " +
                           "7 | . . . . . . . . . " +
                           "8 | . . . . . . . . . " +
                           "9 | . . . . . . . . . ").ToFen());

    Assert.True(board.IsCheck(Color.Black));
  }

  [Test]
  public void IsCheck_NoJumpPieceCannon_ReturnsFalse()
  {
    var board = new Board((string.Empty +
                           "  | 0 1 2 3 4 5 6 7 8 " +
                           "--+------------------- " +
                           "0 | . . . . k . . . . " +
                           "1 | . . . . . . . . . " +
                           "2 | . . . . . . . . . " +
                           "3 | . . . . . . . . . " +
                           "4 | . . . . . . . . . " +
                           "5 | . . . . . . . . . " +
                           "6 | . . . . C . . . . " +
                           "7 | . . . . . . . . . " +
                           "8 | . . . . . . . . . " +
                           "9 | . . . . . . . . . ").ToFen());

    Assert.False(board.IsCheck(Color.Black));
  }

  [Test]
  public void IsCheck_Horse_ReturnsTrue()
  {
    var board = new Board((string.Empty +
                           "  | 0 1 2 3 4 5 6 7 8 " +
                           "--+------------------- " +
                           "0 | . . . . k . . . . " +
                           "1 | . . . . . . . . . " +
                           "2 | . . . . . H . . . " +
                           "3 | . . . . . . . . . " +
                           "4 | . . . . . . . . . " +
                           "5 | . . . . . . . . . " +
                           "6 | . . . . . . . . . " +
                           "7 | . . . . . . . . . " +
                           "8 | . . . . . . . . . " +
                           "9 | . . . . . . . . . ").ToFen());

    Assert.True(board.IsCheck(Color.Black));
  }

  [Test]
  public void IsCheck_PawnBlocksHorse_ReturnsFalse()
  {
    var board = new Board((string.Empty +
                           "  | 0 1 2 3 4 5 6 7 8 " +
                           "--+------------------- " +
                           "0 | . . . . k . . . . " +
                           "1 | . . . . . p . . . " +
                           "2 | . . . . . H . . . " +
                           "3 | . . . . . . . . . " +
                           "4 | . . . . . . . . . " +
                           "5 | . . . . . . . . . " +
                           "6 | . . . . . . . . . " +
                           "7 | . . . . . . . . . " +
                           "8 | . . . . . . . . . " +
                           "9 | . . . . . . . . . ").ToFen());

    Assert.False(board.IsCheck(Color.Black));
  }

  [Test]
  public void IsCheck_Rook_ReturnsTrue()
  {
    var board = new Board((string.Empty +
                           "  | 0 1 2 3 4 5 6 7 8 " +
                           "--+------------------- " +
                           "0 | . . . . k . . . . " +
                           "1 | . . . . . . . . . " +
                           "2 | . . . . . . . . . " +
                           "3 | . . . . . . . . . " +
                           "4 | . . . . . . . . . " +
                           "5 | . . . . R . . . . " +
                           "6 | . . . . . . . . . " +
                           "7 | . . . . . . . . . " +
                           "8 | . . . . . . . . . " +
                           "9 | . . . . . . . . . ").ToFen());

    Assert.True(board.IsCheck(Color.Black));
  }

  [Test]
  public void IsCheck_CannonBlocksRook_ReturnsFalse()
  {
    var board = new Board((string.Empty +
                           "  | 0 1 2 3 4 5 6 7 8 " +
                           "--+------------------- " +
                           "0 | . . . . k . . . . " +
                           "1 | . . . . . . . . . " +
                           "2 | . . . . . . . . . " +
                           "3 | . . . . C . . . . " +
                           "4 | . . . . . . . . . " +
                           "5 | . . . . R . . . . " +
                           "6 | . . . . . . . . . " +
                           "7 | . . . . . . . . . " +
                           "8 | . . . . . . . . . " +
                           "9 | . . . . . . . . . ").ToFen());

    Assert.False(board.IsCheck(Color.Black));
  }

  [Test]
  public void FindOccupiedCellsBy_TwoRedRooks_ReturnsTwoCells()
  {
    var board = new Board((string.Empty +
                           "  | 0 1 2 3 4 5 6 7 8 " +
                           "--+------------------- " +
                           "0 | . . . . . . . . . " +
                           "1 | r . . . . . . . r " +
                           "2 | . . . . . . . . . " +
                           "3 | . . . . . . . . . " +
                           "4 | . . . . . . . . . " +
                           "5 | . . . . . . . . . " +
                           "6 | . . . . . . . . . " +
                           "7 | . . . . . . . . . " +
                           "8 | . . . . . . . . . " +
                           "9 | R . . . . . . . R ").ToFen());

    var attacking = board.FindOccupiedCellsBy(Color.Red);

    Assert.AreEqual(2, attacking.Count);
  }

  [Test]
  public void FindAttackingCellsBy_SharedAttackingCells_ReturnsTwentyFiveCells()
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
                           "9 | R . . . . . . . R ").ToFen());

    var attacking = board.FindAttackingCellsBy(Color.Red);

    Assert.AreEqual(9 + 9 + 7, attacking.Count);
  }

  [Test]
  [TestCase(Compass.N, Compass.N)]
  [TestCase(Compass.W, Compass.W)]
  [TestCase(Compass.N, Compass.N, Compass.N, Compass.N)]
  [TestCase(Compass.NE, Compass.N)]
  [TestCase(Compass.E, Compass.E, Compass.E, Compass.E, Compass.E, Compass.E, Compass.E, Compass.E)]
  [TestCase(Compass.S, Compass.S, Compass.S, Compass.S, Compass.S, Compass.S, Compass.S, Compass.S, Compass.S)]
  public void Cell_RedOriginAtOneOne_OutOfBoundsTests(params Compass[] compasses)
  {
    var board = new Board("9/1K7/9/9/9/9/9/9/9/9");
    var cellRedKing = board.FindCell<King>(Color.Red);

    Assert.Null(board.Cell(cellRedKing, compasses));
  }

  [Test]
  [TestCase(Compass.S, Compass.S)]
  [TestCase(Compass.E, Compass.E)]
  [TestCase(Compass.S, Compass.S, Compass.S, Compass.S)]
  [TestCase(Compass.SW, Compass.S)]
  [TestCase(Compass.W, Compass.W, Compass.W, Compass.W, Compass.W, Compass.W, Compass.W, Compass.W)]
  [TestCase(Compass.N, Compass.N, Compass.N, Compass.N, Compass.N, Compass.N, Compass.N, Compass.N, Compass.N)]
  public void Cell_BlackOriginAtOneOne_OutOfBoundsTests(params Compass[] compasses)
  {
    var board = new Board("9/1k7/9/9/9/9/9/9/9/9");
    var cellBlackKing = board.FindCell<King>(Color.Black);

    Assert.Null(board.Cell(cellBlackKing, compasses));
  }

  [Test]
  [TestCase(1, 0, Compass.N)]
  [TestCase(2, 0, Compass.NE)]
  [TestCase(2, 1, Compass.E)]
  [TestCase(2, 2, Compass.SE)]
  [TestCase(1, 2, Compass.S)]
  [TestCase(0, 2, Compass.SW)]
  [TestCase(0, 1, Compass.W)]
  [TestCase(0, 0, Compass.NW)]
  [TestCase(8, 1, Compass.E, Compass.E, Compass.E, Compass.E, Compass.E, Compass.E, Compass.E)]
  public void Cell_RedOriginAtOneOne_InBoundsTests(int expectedFile, int expectedRank, params Compass[] compasses)
  {
    var board = new Board("9/1K7/9/9/9/9/9/9/9/9");

    // (file: 1, rank: 1)
    var cellRedKing = board.FindCell<King>(Color.Red);

    var newCell = board.Cell(cellRedKing, compasses);

    Assert.AreEqual(new Cell(expectedFile, expectedRank), newCell);
  }

  [Test]
  [TestCase(1, 2, Compass.N)]
  [TestCase(0, 2, Compass.NE)]
  [TestCase(0, 1, Compass.E)]
  [TestCase(0, 0, Compass.SE)]
  [TestCase(1, 0, Compass.S)]
  [TestCase(2, 0, Compass.SW)]
  [TestCase(2, 1, Compass.W)]
  [TestCase(2, 2, Compass.NW)]
  [TestCase(8, 1, Compass.W, Compass.W, Compass.W, Compass.W, Compass.W, Compass.W, Compass.W)]
  public void Cell_BlackOriginAtOneOne_Tests(int expectedFile, int expectedRank, params Compass[] compasses)
  {
    var board = new Board("9/1k7/9/9/9/9/9/9/9/9");

    // (file: 1, rank: 1)
    var cellBlackKing = board.FindCell<King>(Color.Black);

    var newCell = board.Cell(cellBlackKing, compasses);

    Assert.AreEqual(new Cell(expectedFile, expectedRank), newCell);
  }

  [Test]
  public void Cell_FileIndexNegative_ThrowsArgumentException()
  {
    Assert.Throws<ArgumentException>(() => new Board().Cell(-1, 0));
  }

  [Test]
  public void Cell_FileIndexMoreThan8_ThrowsArgumentException()
  {
    Assert.Throws<ArgumentException>(() => new Board().Cell(9, 0));
  }

  [Test]
  public void Cell_RankIndexNegative_ThrowsArgumentException()
  {
    Assert.Throws<ArgumentException>(() => new Board().Cell(0, -1));
  }

  [Test]
  public void Cell_RankIndexMoreThan9_ThrowsArgumentException()
  {
    Assert.Throws<ArgumentException>(() => new Board().Cell(0, 10));
  }

  [Test]
  public void Rank_RankIndexNegative_ThrowsArgumentException()
  {
    Assert.Throws<ArgumentException>(() => new Board().Rank(-1));
  }

  [Test]
  public void Rank_RankIndexMoreThan9_ThrowsArgumentException()
  {
    Assert.Throws<ArgumentException>(() => new Board().Rank(10));
  }

  [Test]
  public void Board_NoParameter_ReturnsStartPosition()
  {
    var board = new Board();

    // Rank index 0
    Assert.AreEqual(new Rook(Color.Black), board.Cell(0, 0).Piece);
    Assert.AreEqual(new Horse(Color.Black), board.Cell(1, 0).Piece);
    Assert.AreEqual(new Elephant(Color.Black), board.Cell(2, 0).Piece);
    Assert.AreEqual(new Adviser(Color.Black), board.Cell(3, 0).Piece);
    Assert.AreEqual(new King(Color.Black), board.Cell(4, 0).Piece);
    Assert.AreEqual(new Adviser(Color.Black), board.Cell(5, 0).Piece);
    Assert.AreEqual(new Elephant(Color.Black), board.Cell(6, 0).Piece);
    Assert.AreEqual(new Horse(Color.Black), board.Cell(7, 0).Piece);
    Assert.AreEqual(new Rook(Color.Black), board.Cell(8, 0).Piece);

    // Rank index 1 (empty rank)
    Assert.True(board.Rank(1).All(c => c.Unoccupied));

    // Rank index 2
    Assert.AreEqual(null, board.Cell(0, 2).Piece);
    Assert.AreEqual(new Cannon(Color.Black), board.Cell(1, 2).Piece);
    Assert.AreEqual(null, board.Cell(2, 2).Piece);
    Assert.AreEqual(null, board.Cell(3, 2).Piece);
    Assert.AreEqual(null, board.Cell(4, 2).Piece);
    Assert.AreEqual(null, board.Cell(5, 2).Piece);
    Assert.AreEqual(null, board.Cell(6, 2).Piece);
    Assert.AreEqual(new Cannon(Color.Black), board.Cell(7, 2).Piece);
    Assert.AreEqual(null, board.Cell(8, 2).Piece);

    // Rank index 3
    Assert.AreEqual(new Pawn(Color.Black), board.Cell(0, 3).Piece);
    Assert.AreEqual(null, board.Cell(1, 3).Piece);
    Assert.AreEqual(new Pawn(Color.Black), board.Cell(2, 3).Piece);
    Assert.AreEqual(null, board.Cell(3, 3).Piece);
    Assert.AreEqual(new Pawn(Color.Black), board.Cell(4, 3).Piece);
    Assert.AreEqual(null, board.Cell(5, 3).Piece);
    Assert.AreEqual(new Pawn(Color.Black), board.Cell(6, 3).Piece);
    Assert.AreEqual(null, board.Cell(7, 3).Piece);
    Assert.AreEqual(new Pawn(Color.Black), board.Cell(8, 3).Piece);

    // Rank index 4 (empty rank)
    Assert.True(board.Rank(4).All(c => c.Unoccupied));

    // Rank index 5 (empty rank)
    Assert.True(board.Rank(5).All(c => c.Unoccupied));

    // Rank index 6
    Assert.AreEqual(new Pawn(Color.Red), board.Cell(0, 6).Piece);
    Assert.AreEqual(null, board.Cell(1, 6).Piece);
    Assert.AreEqual(new Pawn(Color.Red), board.Cell(2, 6).Piece);
    Assert.AreEqual(null, board.Cell(3, 6).Piece);
    Assert.AreEqual(new Pawn(Color.Red), board.Cell(4, 6).Piece);
    Assert.AreEqual(null, board.Cell(5, 6).Piece);
    Assert.AreEqual(new Pawn(Color.Red), board.Cell(6, 6).Piece);
    Assert.AreEqual(null, board.Cell(7, 6).Piece);
    Assert.AreEqual(new Pawn(Color.Red), board.Cell(8, 6).Piece);

    // Rank index 7
    Assert.AreEqual(null, board.Cell(0, 7).Piece);
    Assert.AreEqual(new Cannon(Color.Red), board.Cell(1, 7).Piece);
    Assert.AreEqual(null, board.Cell(2, 7).Piece);
    Assert.AreEqual(null, board.Cell(3, 7).Piece);
    Assert.AreEqual(null, board.Cell(4, 7).Piece);
    Assert.AreEqual(null, board.Cell(5, 7).Piece);
    Assert.AreEqual(null, board.Cell(6, 7).Piece);
    Assert.AreEqual(new Cannon(Color.Red), board.Cell(7, 7).Piece);
    Assert.AreEqual(null, board.Cell(8, 7).Piece);

    // Rank index 8 (empty rank)
    Assert.True(board.Rank(8).All(c => c.Unoccupied));

    // Rank index 9
    Assert.AreEqual(new Rook(Color.Red), board.Cell(0, 9).Piece);
    Assert.AreEqual(new Horse(Color.Red), board.Cell(1, 9).Piece);
    Assert.AreEqual(new Elephant(Color.Red), board.Cell(2, 9).Piece);
    Assert.AreEqual(new Adviser(Color.Red), board.Cell(3, 9).Piece);
    Assert.AreEqual(new King(Color.Red), board.Cell(4, 9).Piece);
    Assert.AreEqual(new Adviser(Color.Red), board.Cell(5, 9).Piece);
    Assert.AreEqual(new Elephant(Color.Red), board.Cell(6, 9).Piece);
    Assert.AreEqual(new Horse(Color.Red), board.Cell(7, 9).Piece);
    Assert.AreEqual(new Rook(Color.Red), board.Cell(8, 9).Piece);
  }
}