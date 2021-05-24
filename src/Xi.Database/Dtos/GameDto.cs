namespace Xi.Database.Dtos
{
  using System;
  using System.Collections.Generic;
  using System.ComponentModel.DataAnnotations;
  using System.Linq;
  using Microsoft.EntityFrameworkCore;
  using Xi.Models.Game;

  [Index(nameof(AcceptedDrawPlayerId), nameof(WinnerPlayerId), nameof(Accepted))]
  public class GameDto
  {
    [Key]
    public int Id { get; set; } = default!;

    [Required]
    public bool Accepted { get; set; } = false;

    [Required]
    public int SecondsPerMove { get; set; } = (int)TimeSpan.FromDays(3).TotalSeconds;

    public int? EloRatingChange { get; set; }

    public DateTime? ClockRunsOutAt { get; set; }

    public int? AcceptedDrawPlayerId { get; set; }

    public PlayerDto? AcceptedDrawPlayer { get; set; } = null;

    [Required]
    public int InitiatedPlayerId { get; set; }

    public PlayerDto InitiatedPlayer { get; set; } = default!;

    [Required]
    public int InvitedPlayerId { get; set; }

    public PlayerDto InvitedPlayer { get; set; } = default!;

    [Required]
    public int RedPlayerId { get; set; }

    public PlayerDto RedPlayer { get; set; } = default!;

    [Required]
    public int BlackPlayerId { get; set; }

    public PlayerDto BlackPlayer { get; set; } = default!;

    public int? WinnerPlayerId { get; set; }

    public PlayerDto? WinnerPlayer { get; set; } = null;

    public List<MoveDto> Moves { get; set; } = new();

    public Game ToGame()
    {
      return new(
        this.Id,
        this.RedPlayer.ToPlayer(),
        this.BlackPlayer.ToPlayer(),
        this.InitiatedPlayer.ToPlayer(),
        this.InvitedPlayer.ToPlayer(),
        this.WinnerPlayer?.ToPlayer(),
        this.SecondsPerMove,
        this.Accepted,
        this.Moves.Select(m => new Move(
          new Cell(m.FromFileIndex, m.FromRankIndex),
          new Cell(m.ToFileIndex, m.ToRankIndex),
          m.CreatedAt))
          .ToList());
    }

    public int TurnPlayerId()
    {
      return this.Moves.Count % 2 == 1 ? this.BlackPlayerId : this.RedPlayerId;
    }

    public PlayerDto TurnPlayer()
    {
      return this.Moves.Count % 2 == 1 ? this.BlackPlayer : this.RedPlayer;
    }
  }
}
