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

      var redPawnCell = board.Find<Pawn>(Color.Red);
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

      var redPawnCell = board.Find<Pawn>(Color.Red);
      var attacking = redPawnCell.Piece!.Attacking(redPawnCell, board);

      Assert.AreEqual(3, attacking.Count);
    }
  }
}