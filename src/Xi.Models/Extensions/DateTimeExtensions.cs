namespace Xi.Models.Extensions
{
  using System;
  using System.Globalization;

  public static class DateTimeExtensions
  {
    public static string SafeFormat(this DateTimeOffset? dateTime, string format)
    {
      if (!dateTime.HasValue)
      {
        return string.Empty;
      }

      return dateTime.Value.ToString(format, new CultureInfo("en-US"));
    }
  }
}