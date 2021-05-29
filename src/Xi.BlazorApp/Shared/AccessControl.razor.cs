namespace Xi.BlazorApp.Shared
{
  using Microsoft.AspNetCore.Components;
  using Xi.BlazorApp.Services;

  public partial class AccessControl
  {
    [Inject]
    private Current Current { get; set; } = default!;

    [Inject]
    private NavigationManager NavigationManager { get; set; } = default!;

    private string ReturnUrl => this.NavigationManager.ToBaseRelativePath(this.NavigationManager.Uri);
  }
}