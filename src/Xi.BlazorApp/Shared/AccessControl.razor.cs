namespace Xi.BlazorApp.Shared
{
  using Microsoft.AspNetCore.Components;
  using Xi.BlazorApp.Services;

  public partial class AccessControl
  {
    [Inject]
    public Current Current { get; set; } = default!;
  }
}