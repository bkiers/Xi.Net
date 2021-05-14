namespace Xi.Database.Dtos
{
  using System.ComponentModel.DataAnnotations;
  using Microsoft.EntityFrameworkCore;

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
    public string PasswordHash { get; set; } = default!;

    [Required]
    public int EloRating { get; set; } = 1000;

    [Required]
    public bool IsAdmin { get; set; } = false;
  }
}
