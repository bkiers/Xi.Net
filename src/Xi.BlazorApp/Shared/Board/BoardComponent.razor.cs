namespace Xi.BlazorApp.Shared.Board
{
  using Microsoft.AspNetCore.Components;
  using Xi.Models.Game;

  public partial class BoardComponent
  {
    [Parameter]
    public Board Board { get; set; } = default!;

    protected override void OnInitialized()
    {
      base.OnInitialized();
    }
  }
}