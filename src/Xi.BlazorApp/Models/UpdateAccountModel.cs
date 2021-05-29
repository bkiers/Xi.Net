namespace Xi.BlazorApp.Models
{
  using System.ComponentModel.DataAnnotations;

  public class UpdateAccountModel
  {
    // TODO playerId

    [Required]
    public bool ShowPossibleMoves { get; set; }
  }
}