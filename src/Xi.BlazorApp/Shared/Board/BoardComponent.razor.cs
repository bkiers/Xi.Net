namespace Xi.BlazorApp.Shared.Board
{
  using System.Collections.Generic;
  using Microsoft.AspNetCore.Components;
  using Xi.BlazorApp.Models;
  using Xi.Models.Game;

  public partial class BoardComponent
  {
    [Parameter]
    public Board Board { get; set; } = default!;

    [Parameter]
    public ISet<Cell> HighlightedCells { get; set; } = default!;

    protected override void OnInitialized()
    {
      base.OnInitialized();
    }
  }
}