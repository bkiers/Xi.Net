namespace Xi.Models.Tests.Game
{
  using System;
  using System.Linq;
  using NUnit.Framework;
  using Xi.Models.Game;

  public class BoardTests
  {
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
}