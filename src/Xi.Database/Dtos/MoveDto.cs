namespace Xi.Database.Dtos
{
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

    [Required]
    public int GameId { get; set; }

    public GameDto Game { get; set; } = default!;
  }
}
