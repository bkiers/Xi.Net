namespace Xi.Models.Extensions;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using Xi.Models.Game;

public static class StringExtensions
{
  public static string ToFen(this string debugString)
  {
    var replacements = new[]
    {
      new[] { @"[\s\d\-+|]", string.Empty },
      new[] { @"(.{9})(?!$)", "$1/" },
      new[] { @"\.{9}", "9" },
      new[] { @"\.{8}", "8" },
      new[] { @"\.{7}", "7" },
      new[] { @"\.{6}", "6" },
      new[] { @"\.{5}", "5" },
      new[] { @"\.{4}", "4" },
      new[] { @"\.{3}", "3" },
      new[] { @"\.{2}", "2" },
      new[] { @"\.{1}", "1" },
    };

    return replacements.Aggregate(debugString, (current, replacement)
      => Regex.Replace(current, replacement[0], replacement[1]));
  }

  public static List<List<Cell>> ParseFen(this string value)
  {
    var cellRows = new List<List<Cell>>();
    var rows = value.Split("/");

    if (rows.Length != 10)
    {
      throw new ArgumentException($"Expected 10 rows, got {rows.Length} in: {value}");
    }

    var rankIndex = 0;

    foreach (var row in rows)
    {
      // Replace all numbers with an equal amount of dots.
      var normalizedRow = string.Join(
        string.Empty,
        row.Select(c => char.IsDigit(c) ?
          new string(char.Parse(Piece.NoPiece), int.Parse(c.ToString())) :
          c.ToString()));

      if (normalizedRow.Length != 9)
      {
        throw new ArgumentException($"Expected 9 values in row {row}, got {normalizedRow.Length}");
      }

      cellRows.Add(normalizedRow.Select((c, i) => new Cell(i, rankIndex, Piece.Parse(c))).ToList());

      rankIndex++;
    }

    return cellRows;
  }
}