namespace Xi.Database.Dtos;

using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

[Index(nameof(GameId))]
public class MoveDto
{
  [Key]
  public int Id { get; set; } = default!;

  [Required]
  public int FromRankIndex { get; set; }

  [Required]
  public int FromFileIndex { get; set; }

  [Required]
  public int ToRankIndex { get; set; }

  [Required]
  public int ToFileIndex { get; set; }

  public string? CapturedPiece { get; set; }

  [Required]
  public int GameId { get; set; }

  [Required]
  public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

  public GameDto Game { get; set; } = default!;
}