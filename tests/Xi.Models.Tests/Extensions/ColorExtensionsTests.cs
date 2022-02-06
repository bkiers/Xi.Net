namespace Xi.Models.Tests.Extensions;

using NUnit.Framework;
using Xi.Models.Extensions;
using Xi.Models.Game;

public class ColorExtensionsTests
{
  [Test]
  public void IsRed_Red_ReturnsTrue()
  {
    Assert.True(Color.Red.IsRed());
  }

  [Test]
  public void IsRed_Black_ReturnsFalse()
  {
    Assert.False(Color.Black.IsRed());
  }

  [Test]
  public void IsBlack_Red_ReturnsFalse()
  {
    Assert.False(Color.Red.IsBlack());
  }

  [Test]
  public void IsBlack_Black_ReturnsTrue()
  {
    Assert.True(Color.Black.IsBlack());
  }

  [Test]
  public void Opposite_Red_ReturnsBlack()
  {
    Assert.AreEqual(Color.Black, Color.Red.Opposite());
  }

  [Test]
  public void Opposite_Black_ReturnsRed()
  {
    Assert.AreEqual(Color.Red, Color.Black.Opposite());
  }
}