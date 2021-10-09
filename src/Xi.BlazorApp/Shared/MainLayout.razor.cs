namespace Xi.BlazorApp.Shared
{
  using Fluxor;
  using Microsoft.AspNetCore.Components;
  using MudBlazor;
  using Xi.BlazorApp.Services;
  using Xi.BlazorApp.Stores.Features.Account.Actions.UpdateAccount;
  using Xi.Models;

  public partial class MainLayout
  {
    private static readonly MudTheme DefaultTheme = new()
    {
      Palette = new Palette
      {
        Primary = Colors.BlueGrey.Darken1,
        Secondary = Colors.Blue.Darken4,
        AppbarBackground = Colors.BlueGrey.Darken1,
      },
    };

    private static readonly MudTheme DarkTheme = new()
    {
      Palette = new Palette
      {
        Black = "#27272f",
        Background = "#32333d",
        BackgroundGrey = "#27272f",
        Surface = "#373740",
        DrawerBackground = "#27272f",
        DrawerText = "rgba(255,255,255, 0.50)",
        DrawerIcon = "rgba(255,255,255, 0.50)",
        AppbarBackground = "#27272f",
        AppbarText = "rgba(255,255,255, 0.70)",
        TextPrimary = "rgba(255,255,255, 0.70)",
        TextSecondary = "rgba(255,255,255, 0.50)",
        ActionDefault = "#adadb1",
        ActionDisabled = "rgba(255,255,255, 0.26)",
        ActionDisabledBackground = "rgba(255,255,255, 0.12)",
        Divider = "rgba(255,255,255, 0.12)",
        DividerLight = "rgba(255,255,255, 0.06)",
        TableLines = "rgba(255,255,255, 0.12)",
        LinesDefault = "rgba(255,255,255, 0.12)",
        LinesInputs = "rgba(255,255,255, 0.3)",
        TextDisabled = "rgba(255,255,255, 0.2)",
      },
    };

    private MudTheme? theme;
    private Settings userSettings = new();
    private bool loggedIn;
    private bool drawerOpen;
    private bool darkMode;

    [Inject]
    private IDispatcher Dispatcher { get; set; } = default!;

    [Inject]
    private Current Current { get; set; } = default!;

    protected override void OnInitialized()
    {
      base.OnInitialized();

      this.loggedIn = this.Current.LoggedIn();
      this.userSettings = this.loggedIn ? this.Current.Settings() : new Settings();
      this.drawerOpen = this.userSettings.ExpandMenu;
      this.darkMode = this.userSettings.DarkMode;
      this.theme = this.darkMode ? DarkTheme : DefaultTheme;
    }

    private void ToggleDrawer()
    {
      this.drawerOpen = !this.drawerOpen;

      if (this.loggedIn)
      {
        this.userSettings.ExpandMenu = this.drawerOpen;

        this.Dispatcher.Dispatch(new UpdateAccountSettingsAction(this.Current.LoggedInPlayerId(), this.userSettings));
      }
    }

    private void ToggleDarkMode()
    {
      this.darkMode = !this.darkMode;
      this.theme = this.darkMode ? DarkTheme : DefaultTheme;

      if (this.loggedIn)
      {
        this.userSettings.DarkMode = this.darkMode;

        this.Dispatcher.Dispatch(new UpdateAccountSettingsAction(this.Current.LoggedInPlayerId(), this.userSettings));
      }
    }
  }
}