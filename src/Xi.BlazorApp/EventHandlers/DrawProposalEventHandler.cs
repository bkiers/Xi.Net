namespace Xi.BlazorApp.EventHandlers
{
  using System.Threading.Tasks;
  using JKang.EventBus;
  using Microsoft.Extensions.Logging;
  using Xi.BlazorApp.Models;
  using Xi.BlazorApp.Services;

  public class DrawProposalEventHandler : IEventHandler<DrawProposalEventHandler.Event>
  {
    private readonly IEmailService emailService;
    private readonly ILogger<DrawProposalEventHandler> logger;

    public DrawProposalEventHandler(IEmailService emailService, ILogger<DrawProposalEventHandler> logger)
    {
      this.emailService = emailService;
      this.logger = logger;
    }

    public Task HandleEventAsync(Event @event)
    {
      this.logger.LogDebug($">>> event: {@event}");

      var model = @event.GameModel;

      return this.emailService.Send(EmailTemplateType.DrawProposal, model.OpponentOf(model.Game.ProposedDrawPlayer!), model);
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