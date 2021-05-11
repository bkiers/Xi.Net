namespace Xi.Models.Tests.Game
{
  using NUnit.Framework;
  using Xi.Models.Extensions;
  using Xi.Models.Game;

  public class PawnTests
  {
    [Test]
    public void Attacking_OnOwnSide_ReturnsSingleCell()
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
        "6 | . . . . P . . . . " +
        "7 | . . . . . . . . . " +
        "8 | . . . . . . . . . " +
        "9 | . . . . . . . . . ").ToFen());

      var redPawnCell = board.FindCell<Pawn>(Color.Red);
      var attacking = redPawnCell.Piece!.Attacking(redPawnCell, board);

      Assert.AreEqual(1, attacking.Count);
    }

    [Test]
    public void Attacking_OnOwnSideOwnPieceInFront_ReturnsNoCells()
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
         "6 | . . . . P . . . . " +
         "7 | . . . . . . . . . " +
         "8 | . . . . . . . . . " +
         "9 | . . . . . . . . . ").ToFen());

      var redPawnCell = board.FindCell<Pawn>(Color.Red);
      var attacking = redPawnCell.Piece!.Attacking(redPawnCell, board);

      Assert.AreEqual(0, attacking.Count);
    }

    [Test]
    public void Attacking_OnOwnSideEnemyPieceInFront_ReturnsOneCell()
    {
      var board = new Board((string.Empty +
         "  | 0 1 2 3 4 5 6 7 8 " +
         "--+------------------- " +
         "0 | . . . . . . . . . " +
         "1 | . . . . . . . . . " +
         "2 | . . . . . . . . . " +
         "3 | . . . . . . . . . " +
         "4 | . . . . . . . . . " +
         "5 | . . . . c . . . . " +
         "6 | . . . . P . . . . " +
         "7 | . . . . . . . . . " +
         "8 | . . . . . . . . . " +
         "9 | . . . . . . . . . ").ToFen());

      var redPawnCell = board.FindCell<Pawn>(Color.Red);
      var attacking = redPawnCell.Piece!.Attacking(redPawnCell, board);

      Assert.AreEqual(1, attacking.Count);
    }

    [Test]
    public void Attacking_OnEnemySide_ReturnsThreeCells()
    {
      var board = new Board((string.Empty +
         "  | 0 1 2 3 4 5 6 7 8 " +
         "--+------------------- " +
         "0 | . . . . . . . . . " +
         "1 | . . . . . . . . . " +
         "2 | . . . . . . . . . " +
         "3 | . . . . . . . . . " +
         "4 | . . . . P . . . . " +
         "5 | . . . . . . . . . " +
         "6 | . . . . . . . . . " +
         "7 | . . . . . . . . . " +
         "8 | . . . . . . . . . " +
         "9 | . . . . . . . . . ").ToFen());

      var redPawnCell = board.FindCell<Pawn>(Color.Red);
      var attacking = redPawnCell.Piece!.Attacking(redPawnCell, board);

      Assert.AreEqual(3, attacking.Count);
    }

    [Test]
    public void Attacking_OnEnemySideSurroundedByOwnPieces_ReturnsNoCells()
    {
      var board = new Board((string.Empty +
         "  | 0 1 2 3 4 5 6 7 8 " +
         "--+------------------- " +
         "0 | . . . . . . . . . " +
         "1 | . . . . . . . . . " +
         "2 | . . . . . . . . . " +
         "3 | . . . . C . . . . " +
         "4 | . . . C P H . . . " +
         "5 | . . . . . . . . . " +
         "6 | . . . . . . . . . " +
         "7 | . . . . . . . . . " +
         "8 | . . . . . . . . . " +
         "9 | . . . . . . . . . ").ToFen());

      var redPawnCell = board.FindCell<Pawn>(Color.Red);
      var attacking = redPawnCell.Piece!.Attacking(redPawnCell, board);

      Assert.AreEqual(0, attacking.Count);
    }

    [Test]
    public void Attacking_OnEnemySideSurroundedByEnemyPieces_ReturnsThreeCells()
    {
      var board = new Board((string.Empty +
         "  | 0 1 2 3 4 5 6 7 8 " +
         "--+------------------- " +
         "0 | . . . . . . . . . " +
         "1 | . . . . . . . . . " +
         "2 | . . . . . . . . . " +
         "3 | . . . . c . . . . " +
         "4 | . . . c P h . . . " +
         "5 | . . . . . . . . . " +
         "6 | . . . . . . . . . " +
         "7 | . . . . . . . . . " +
         "8 | . . . . . . . . . " +
         "9 | . . . . . . . . . ").ToFen());

      var redPawnCell = board.FindCell<Pawn>(Color.Red);
      var attacking = redPawnCell.Piece!.Attacking(redPawnCell, board);

      Assert.AreEqual(3, attacking.Count);
    }
  }
}