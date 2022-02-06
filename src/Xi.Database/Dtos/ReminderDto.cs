namespace Xi.Database.Dtos;

using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

[Index(nameof(GameId), nameof(MoveNumber))]
public class ReminderDto
{
  [Key]
  public int Id { get; set; } = default!;

  [Required]
  public int GameId { get; set; }

  [Required]
  public int MoveNumber { get; set; }

  public GameDto Game { get; set; } = default!;
}