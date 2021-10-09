namespace Xi.BlazorApp.Shared.Board
{
  using System.Collections;
  using System.Collections.Generic;
  using System.Linq;
  using Microsoft.AspNetCore.Components;
  using Xi.BlazorApp.Models;
  using Xi.Models.Extensions;
  using Xi.Models.Game;

  public partial class CapturedPiecesComponent
  {
    [Parameter]
    public GameModel GameModel { get; set; } = default!;

    [Parameter]
    public Color CurrentColor { get; set; } = default!;

    private IEnumerable<string> CapturedPieces()
    {
      return this.GameModel.Game.Moves
        .For(this.CurrentColor)
        .Where(m => m.CapturedPiece != null)
        .Select(m => m.CapturedPiece!);
    }
  }
}