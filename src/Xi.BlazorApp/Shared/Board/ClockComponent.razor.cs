namespace Xi.BlazorApp.Shared.Board
{
  using System;
  using System.Threading.Tasks;
  using Microsoft.AspNetCore.Components;

  public partial class ClockComponent
  {
    [Parameter]
    public DateTimeOffset ClockRunsOutAt { get; set; }

    [Parameter]
    public bool Large { get; set; } = true;

    private string? FormattedCounter { get; set; }

    protected override async Task OnInitializedAsync()
    {
      await base.OnInitializedAsync();

      this.CountDown();
    }

    private async void CountDown()
    {
      while ((this.ClockRunsOutAt - DateTimeOffset.UtcNow).TotalSeconds >= 0)
      {
        this.FormattedCounter = this.GetFormattedCounter();
        this.StateHasChanged();

        await Task.Delay(1000);
      }
    }

    private string GetFormattedCounter()
    {
      var remaining = this.ClockRunsOutAt - DateTimeOffset.UtcNow;

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