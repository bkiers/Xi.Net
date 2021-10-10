namespace Xi.BlazorApp.EventHandlers
{
  using System;
  using System.Threading.Tasks;
  using JKang.EventBus;
  using Microsoft.Extensions.Logging;
  using Xi.BlazorApp.Models;
  using Xi.BlazorApp.Services;
  using Xi.Models.Game;

  public class GameOverEventHandler : IEventHandler<GameOverEventHandler.Event>
  {
    private readonly IEmailService emailService;
    private readonly ILogger<GameOverEventHandler> logger;

    public GameOverEventHandler(IEmailService emailService, ILogger<GameOverEventHandler> logger)
    {
      this.emailService = emailService;
      this.logger = logger;
    }

    public async Task HandleEventAsync(Event @event)
    {
      this.logger.LogDebug($">>> GameOverEventHandler event: game {@event.GameModel.Game.Id}");

      var winner = @event.GameModel.Game.WinnerPlayer;
      var loser = winner == null ? null : @event.GameModel.OpponentOf(winner);
      var acceptedDraw = @event.GameModel.Game.AcceptedDrawPlayer;
      var proposedDraw = acceptedDraw == null ? null : @event.GameModel.OpponentOf(acceptedDraw);

      switch (@event.GameModel.Game.GameResultType)
      {
        case GameResultType.Checkmate:
        case GameResultType.Stalemate:
          await this.emailService.Send(EmailTemplateType.GameOver, loser!, @event.GameModel);
          break;
        case GameResultType.Draw:
          await this.emailService.Send(EmailTemplateType.GameOverDraw, proposedDraw!, @event.GameModel);
          break;
        case GameResultType.TimeUp:
          await this.emailService.Send(EmailTemplateType.GameOverTimeUp, loser!, @event.GameModel);
          await this.emailService.Send(EmailTemplateType.GameOverTimeUp, winner!, @event.GameModel);
          break;
        case GameResultType.Forfeit:
          await this.emailService.Send(EmailTemplateType.Forfeited, winner!, @event.GameModel);
          break;
        default:
          throw new Exception($"Unhandled type: {@event.GameModel.Game.GameResultType}");
      }
    }

    public class Event : GameEvent
    {
      public Event(GameModel gameModel)
        : base(gameModel)
      {
      }
    }
  }
}