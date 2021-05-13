namespace Xi.BlazorApp.Shared.Board
{
  using Microsoft.AspNetCore.Components;
  using Xi.Models.Game;

  public partial class PieceComponent
  {
    [Parameter]
    public Piece? Piece { get; set; }

    public string PieceUrl => this.Piece == null ?
      string.Empty :
      $"/images/pieces/{this.Piece!.AsciiNotation.ToLower()}{this.Piece!.Color.ToString().ToLower()[0]}.svg";

    protected override void OnInitialized()
    {
      base.OnInitialized();
    }
  }
}