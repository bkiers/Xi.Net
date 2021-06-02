namespace Xi.BlazorApp.Shared
{
  using System;
  using System.Threading.Tasks;
  using Microsoft.AspNetCore.Components;

  public partial class Alert
  {
    public enum AlertType
    {
      Danger,
      Warning,
      Info,
    }

    [Parameter]
    public string Message { get; set; } = default!;

    [Parameter]
    public AlertType Type { get; set; } = AlertType.Info;

    [Parameter]
    public bool Dismissible { get; set; } = true;

    protected override Task OnInitializedAsync()
    {
      if (this.Dismissible)
      {
        this.DismissAfter(TimeSpan.FromSeconds(5));
      }

      return base.OnInitializedAsync();
    }

    private async void DismissAfter(TimeSpan timeSpan)
    {
      await Task.Delay(timeSpan);
      this.Message = string.Empty;
      this.StateHasChanged();
    }
  }
}