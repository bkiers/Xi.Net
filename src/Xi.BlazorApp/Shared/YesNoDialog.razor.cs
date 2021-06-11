namespace Xi.BlazorApp.Shared
{
  using Microsoft.AspNetCore.Components;
  using MudBlazor;

  public partial class YesNoDialog
  {
    [Parameter]
    public string Title { get; set; } = default!;

    [Parameter]
    public string Content { get; set; } = default!;

    [Parameter]
    public string YesText { get; set; } = "Yes";

    [Parameter]
    public string NoText { get; set; } = "No";

    [Parameter]
    public bool InverseButtonColors { get; set; }

    [CascadingParameter]
    private MudDialogInstance MudDialog { get; set; } = default!;

    private Color YesColor => this.InverseButtonColors ? Color.Error : Color.Success;

    private Color NoColor => this.InverseButtonColors ? Color.Success : Color.Error;

    private void Yes()
    {
      this.MudDialog.Close();
    }

    private void No()
    {
      this.MudDialog.Cancel();
    }
  }
}