namespace Xi.Models.Tests.Game
{
  using NUnit.Framework;
  using Xi.Models.Game;

  public class PlayerTests
  {
    [Test]
    public void ProcessEloPoints_WinWithSameRating_ChangesEqualPoints()
    {
      var won = new Player(1, "name1", "email1", 1000, false, false);
      var lost = new Player(1, "name2", "email2", 1000, false, false);

      won.ProcessEloPoints(lost, false);

      Assert.That(won.EloRating, Is.EqualTo(1016));
      Assert.That(lost.EloRating, Is.EqualTo(984));
    }

    [Test]
    public void ProcessEloPoints_DrawWithSameRating_ChangesEqualPoints()
    {
      var won = new Player(1, "name1", "email1", 1000, false, false);
      var lost = new Player(1, "name2", "email2", 1000, false, false);

      won.ProcessEloPoints(lost, true);

      Assert.That(won.EloRating, Is.EqualTo(1000));
      Assert.That(lost.EloRating, Is.EqualTo(1000));
    }

    [Test]
    public void ProcessEloPoints_WinWithHighRating_ChangesFewPoints()
    {
      var won = new Player(1, "name1", "email1", 1100, false, false);
      var lost = new Player(1, "name2", "email2", 1000, false, false);

      won.ProcessEloPoints(lost, false);

      Assert.That(won.EloRating, Is.EqualTo(1112));
      Assert.That(lost.EloRating, Is.EqualTo(988));
    }

    [Test]
    public void ProcessEloPoints_DrawWithHighRating_ChangesFewPoints()
    {
      var won = new Player(1, "name1", "email1", 1100, false, false);
      var lost = new Player(1, "name2", "email2", 1000, false, false);

      won.ProcessEloPoints(lost, true);

      Assert.That(won.EloRating, Is.EqualTo(1096));
      Assert.That(lost.EloRating, Is.EqualTo(1004));
    }

    [Test]
    public void ProcessEloPoints_WinWithLowRating_ChangesMorePoints()
    {
      var won = new Player(1, "name1", "email1", 1000, false, false);
      var lost = new Player(1, "name2", "email2", 1100, false, false);

      won.ProcessEloPoints(lost, false);

      Assert.That(won.EloRating, Is.EqualTo(1020));
      Assert.That(lost.EloRating, Is.EqualTo(1080));
    }
  }
}