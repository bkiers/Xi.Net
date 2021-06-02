namespace Xi.BlazorApp.Shared.Board
{
  using System;
  using System.Threading.Tasks;
  using Microsoft.AspNetCore.Components;

  public partial class ClockComponent
  {
    [Parameter]
    public DateTime ClockRunsOutAt { get; set; }

    private string? FormattedCounter { get; set; }

    protected override async Task OnInitializedAsync()
    {
      this.CountDown();

      await base.OnInitializedAsync();
    }

    private async void CountDown()
    {
      while ((this.ClockRunsOutAt - DateTime.UtcNow).TotalSeconds >= 0)
      {
        this.FormattedCounter = this.GetFormattedCounter();
        this.StateHasChanged();

        await Task.Delay(1000);
      }
    }

    private string GetFormattedCounter()
    {
      var remaining = this.ClockRunsOutAt - DateTime.UtcNow;

      if (remaining < TimeSpan.FromDays(1))
      {
        return remaining.ToString(@"'0 days 'hh\:mm\:ss");
      }

      if (remaining < TimeSpan.FromDays(2))
      {
        return remaining.ToString(@"d' day 'hh\:mm\:ss");
      }

      return remaining.ToString(@"d' days 'hh\:mm\:ss");
    }
  }
}