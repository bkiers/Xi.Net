namespace Xi.BlazorApp.BackgroundServices
{
  using System;
  using System.Linq;
  using System.Threading;
  using System.Threading.Tasks;
  using JKang.EventBus;
  using Microsoft.Extensions.DependencyInjection;
  using Microsoft.Extensions.Logging;
  using Xi.BlazorApp.Models;
  using Xi.BlazorApp.Services;

  public class TimeCheckService : BackgroundService
  {
    private readonly IServiceCollection services;
    private readonly ILogger<TimeCheckService> logger;

    public TimeCheckService(IServiceCollection services, ILogger<TimeCheckService> logger)
    {
      this.services = services;
      this.logger = logger;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
      this.logger.LogDebug("Started");

      stoppingToken.Register(() => this.logger.LogDebug("Terminated"));

      while (!stoppingToken.IsCancellationRequested)
      {
        using (IServiceScope scope = this.services.BuildServiceProvider().CreateScope())
        {
          var eventPublisher = scope.ServiceProvider.GetRequiredService<IEventPublisher>();
          var gameService = scope.ServiceProvider.GetService<IGameService>()!;

          foreach (var game in gameService.UnfinishedGames())
          {
            var hoursThinkingTime = (game.Game.ClockRunsOutAt - DateTime.UtcNow)!.Value.TotalHours;

            switch (hoursThinkingTime)
            {
              case < 0:
                await eventPublisher.PublishEventAsync(new GameEvent(game));
                break;
              case < 12 when game.Game.Reminders.SingleOrDefault(r => r.MoveNumber == game.Game.Moves.Count) == null:
                await eventPublisher.PublishEventAsync(new GameEvent(game));
                break;
            }
          }
        }

        await Task.Delay(TimeSpan.FromSeconds(60), stoppingToken);
      }

      this.logger.LogDebug("Stopped");
    }
  }
}