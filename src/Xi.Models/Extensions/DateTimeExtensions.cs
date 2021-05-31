namespace Xi.Models.Extensions
{
  using System;

  public static class DateTimeExtensions
  {
    private static readonly TimeZoneInfo NlTimeZoneInfo = OperatingSystem.IsWindows() ?
      TimeZoneInfo.FindSystemTimeZoneById("W. Europe Standard Time") :
      TimeZoneInfo.FindSystemTimeZoneById("Europe/Amsterdam");

    public static string ToStringNL(this DateTime? dateTime, string format)
    {
      if (!dateTime.HasValue)
      {
        return string.Empty;
      }

      var uctDateTime = DateTime.SpecifyKind(dateTime.Value, DateTimeKind.Utc);
      DateTimeOffset offset = uctDateTime;

      return offset
        .ToOffset(NlTimeZoneInfo.GetUtcOffset(uctDateTime))
        .ToString(format);
    }
  }
}