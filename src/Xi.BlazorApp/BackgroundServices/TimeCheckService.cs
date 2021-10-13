namespace Xi.BlazorApp.BackgroundServices
{
  using System;
  using System.Linq;
  using System.Threading;
  using System.Threading.Tasks;
  using JKang.EventBus;
  using Microsoft.Extensions.DependencyInjection;
  using Microsoft.Extensions.Logging;
  using Xi.BlazorApp.EventHandlers;
  using Xi.BlazorApp.Models;
  using Xi.BlazorApp.Services;
  using Xi.Database;
  using Xi.Database.Dtos;
  using Xi.Models.Game;

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
      // Since this BackgroundService uses the database, first let possible migrations run before starting.
      await Task.Delay(TimeSpan.FromSeconds(30), stoppingToken);

      this.logger.LogDebug("Started");

      stoppingToken.Register(() => this.logger.LogDebug("Terminated"));

      while (!stoppingToken.IsCancellationRequested)
      {
        using (IServiceScope scope = this.services.BuildServiceProvider().CreateScope())
        {
          var eventPublisher = scope.ServiceProvider.GetRequiredService<IEventPublisher>();
          var gameService = scope.ServiceProvider.GetService<IGameService>()!;
          var db = scope.ServiceProvider.GetService<XiContext>()!;

          foreach (var model in gameService.UnfinishedGames())
          {
            var hoursThinkingTime = (model.Game.ClockRunsOutAt - DateTimeOffset.UtcNow)!.Value.TotalHours;

            this.logger.LogDebug($"Game {model.Game.Id}: hoursThinkingTime={hoursThinkingTime}");

            switch (hoursThinkingTime)
            {
              case < 0:
                // Time's up!
                var updated = gameService.EndGame(model.Game.Id, model.OpponentOf(model.Game.TurnPlayer()).Id, GameResultType.TimeUp);
                await eventPublisher.PublishEventAsync(new GameOverEventHandler.Event(updated));
                break;
              case < 12 when model.Game.Reminders.SingleOrDefault(r => r.MoveNumber == model.Game.Moves.Count) == null:
                // Less than 12 hours, and no reminder was sent yet.
                db.Reminders.Add(new ReminderDto { GameId = model.Game.Id, MoveNumber = model.Game.Moves.Count });
                await db.SaveChangesAsync(stoppingToken);
                await eventPublisher.PublishEventAsync(new SendEmailEventHandler.Event(EmailTemplateType.MoveReminder, model.Game.TurnPlayer(), model));
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