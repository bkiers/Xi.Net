namespace Xi.Database.Dtos;

using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;
using Xi.Models.Game;

[Index(nameof(Email), IsUnique = true)]
public class PlayerDto
{
  [Key]
  public int Id { get; set; } = default!;

  [Required]
  public string Name { get; set; } = default!;

  [Required]
  public string Email { get; set; } = default!;

  [Required]
  public int EloRating { get; set; } = 1000;

  public DateTimeOffset? LastSeenOn { get; set; }

  [Required]
  public string? SettingsJson { get; set; }

  [Required]
  public bool IsAdmin { get; set; } = false;

  public Player ToPlayer()
  {
    return new Player(
      this.Id,
      this.Name,
      this.Email,
      this.EloRating,
      this.LastSeenOn,
      this.SettingsJson,
      this.IsAdmin);
  }
}