namespace Xi.BlazorApp.Shared;

using Microsoft.AspNetCore.Components;
using MudBlazor;

public partial class InfoDialog
{
  [Parameter]
  public string Title { get; set; } = default!;

  [Parameter]
  public string Content { get; set; } = default!;

  [CascadingParameter]
  private MudDialogInstance MudDialog { get; set; } = default!;

  private void Close()
  {
    this.MudDialog.Close();
  }
}