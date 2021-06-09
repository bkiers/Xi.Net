namespace Xi.BlazorApp.Shared
{
  public partial class MainLayout
  {
    private bool drawerOpen = true;

    private void ToggleDrawer()
    {
      this.drawerOpen = !this.drawerOpen;
    }
  }
}