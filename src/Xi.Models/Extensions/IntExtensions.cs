namespace Xi.Models.Extensions
{
  using System;

  public static class IntExtensions
  {
    public static int ToDays(this int seconds)
    {
      return (int)TimeSpan.FromSeconds(seconds).TotalDays;
    }
  }
}