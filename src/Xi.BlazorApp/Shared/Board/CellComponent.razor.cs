namespace Xi.BlazorApp.Shared.Board
{
  using System;
  using Microsoft.AspNetCore.Components;
  using Xi.Models.Game;

  public partial class CellComponent
  {
    [Parameter]
    public Cell Cell { get; set; } = default!;

    public string ImageUrl => $"/images/board/{this.Cell.RankIndex + 1}_{this.Cell.FileIndex + 1}.png";

    public void CellClicked()
    {
      // TODO
    }

    protected override void OnInitialized()
    {
      base.OnInitialized();
    }
  }
}