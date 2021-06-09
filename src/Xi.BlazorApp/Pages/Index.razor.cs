namespace Xi.BlazorApp.Pages
{
  using Microsoft.AspNetCore.Components;

  public partial class Index
  {
    [Inject]
    private NavigationManager NavigationManager { get; set; } = default!;

    protected override void OnInitialized()
    {
      base.OnInitialized();

      this.NavigationManager.NavigateTo("/home");
    }
  }
}