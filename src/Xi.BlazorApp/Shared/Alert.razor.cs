namespace Xi.BlazorApp.Shared
{
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
  }
}