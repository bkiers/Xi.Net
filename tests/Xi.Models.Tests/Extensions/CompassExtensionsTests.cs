namespace Xi.Models.Tests.Extensions;

using NUnit.Framework;
using Xi.Models.Extensions;
using Xi.Models.Game;

public class CompassExtensionsTests
{
  [Test]
  [TestCase(Compass.N, 0, -1)]
  [TestCase(Compass.NE, 1, -1)]
  [TestCase(Compass.E, 1, 0)]
  [TestCase(Compass.SE, 1, 1)]
  [TestCase(Compass.S, 0, 1)]
  [TestCase(Compass.SW, -1, 1)]
  [TestCase(Compass.W, -1, 0)]
  [TestCase(Compass.NW, -1, -1)]
  public void DeltaFileRank_Red_Tests(Compass compass, int expectedDeltaFile, int expectedDeltaRank)
  {
    var piece = new Pawn(Color.Red);

    Assert.AreEqual((expectedDeltaFile, expectedDeltaRank), compass.DeltaFileRank(piece));
  }

  [Test]
  [TestCase(Compass.N, 0, 1)]
  [TestCase(Compass.NE, -1, 1)]
  [TestCase(Compass.E, -1, 0)]
  [TestCase(Compass.SE, -1, -1)]
  [TestCase(Compass.S, 0, -1)]
  [TestCase(Compass.SW, 1, -1)]
  [TestCase(Compass.W, 1, 0)]
  [TestCase(Compass.NW, 1, 1)]
  public void DeltaFileRank_Black_Tests(Compass compass, int expectedDeltaFile, int expectedDeltaRank)
  {
    var piece = new Pawn(Color.Black);

    Assert.AreEqual((expectedDeltaFile, expectedDeltaRank), compass.DeltaFileRank(piece));
  }
}