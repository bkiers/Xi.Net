namespace Xi.BlazorApp.Models;

using System.ComponentModel.DataAnnotations;
using Xi.Models.Game;

public class NewGameModel
{
  [Required]
  public Color PlayingWithColor { get; set; }

  [Required]
  public int OpponentPlayerId { get; set; }

  [Required]
  [Range(1, 7, ErrorMessage = "The amount of days must be between 1 and 7.")]
  public int DaysPerMove { get; set; }
}