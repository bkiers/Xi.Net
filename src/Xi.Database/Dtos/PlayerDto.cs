namespace Xi.Database.Dtos
{
  using System.ComponentModel.DataAnnotations;
  using Microsoft.EntityFrameworkCore;
  using Newtonsoft.Json;
  using Xi.Models;
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

    [Required]
    public bool IsAdmin { get; set; } = false;

    [Required]
    public string? SettingsJson { get; set; }

    public Player ToPlayer()
    {
      return new Player(
        this.Id,
        this.Name,
        this.Email,
        this.EloRating,
        this.SettingsJson,
        this.IsAdmin);
    }
  }
}
