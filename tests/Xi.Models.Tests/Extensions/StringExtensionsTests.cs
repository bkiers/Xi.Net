namespace Xi.Models.Tests.Extensions
{
  using System;
  using NUnit.Framework;
  using Xi.Models.Extensions;
  using Xi.Models.Game;

  public class StringExtensionsTests
  {
    [Test]
    public void ToFen_DebugBoardWithIndexes_ReturnsFenNotation()
    {
      var debugString =
        "  | 0 1 2 3 4 5 6 7 8" +
        "--+------------------" +
        "0 | r h e a k a e h r" +
        "1 | . . . . . . . . ." +
        "2 | . c . . . . . c ." +
        "3 | p . p . p . p . p" +
        "4 | . . . . . . . . ." +
        "5 | . . . . . . . . ." +
        "6 | P . P . P . P . P" +
        "7 | . C . . . . . C ." +
        "8 | . . . . . . . . ." +
        "9 | R H E A K A E H R";

      Assert.AreEqual("rheakaehr/9/1c5c1/p1p1p1p1p/9/9/P1P1P1P1P/1C5C1/9/RHEAKAEHR", debugString.ToFen());
    }

    [Test]
    public void ParseFen_ValidFenNotation_ShouldReturnCells()
    {
      Assert.NotNull(Board.StartFenNotation.ParseFen());
    }

    [Test]
    public void ParseFen_TooFewRanks_ShouldThrowArgumentException()
    {
      Assert.Throws<ArgumentException>(() => "9/9/9/9/9/9/9/9/9".ParseFen());
    }

    [Test]
    public void ParseFen_TooManyRanks_ShouldThrowArgumentException()
    {
      Assert.Throws<ArgumentException>(() => "9/9/9/9/9/9/9/9/9/9/9".ParseFen());
    }

    [Test]
    public void ParseFen_UnknownPiece_ShouldThrowArgumentException()
    {
      Assert.Throws<ArgumentException>(() => "x8/9/9/9/9/9/9/9/9/9".ParseFen());
    }

    [Test]
    public void ParseFen_TooFewFiles_ShouldThrowArgumentException()
    {
      Assert.Throws<ArgumentException>(() => "8/9/9/9/9/9/9/9/9".ParseFen());
    }

    [Test]
    public void ParseFen_TooManyFiles_ShouldThrowArgumentException()
    {
      Assert.Throws<ArgumentException>(() => "4k5/9/9/9/9/9/9/9/9".ParseFen());
    }
  }
}