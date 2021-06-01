namespace Xi.BlazorApp.Shared
{
  public partial class NavMenu
  {
    private bool CollapseNavMenu { get; set; } = true;

    private string? NavMenuCssClass => this.CollapseNavMenu ? "collapse" : null;

    private void ToggleNavMenu()
    {
      this.CollapseNavMenu = !this.CollapseNavMenu;
    }
  }
}