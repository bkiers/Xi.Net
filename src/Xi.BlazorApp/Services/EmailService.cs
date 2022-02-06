namespace Xi.BlazorApp.Services;

using System;
using System.Dynamic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Mailjet.Client;
using Mailjet.Client.TransactionalEmails;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Xi.BlazorApp.Config;
using Xi.BlazorApp.Models;
using Xi.Models.Extensions;
using Xi.Models.Game;

public class EmailService : IEmailService
{
  private readonly XiConfig config;
  private readonly ILogger<EmailService> logger;

  public EmailService(IOptions<XiConfig> options, ILogger<EmailService> logger)
  {
    this.config = options.Value;
    this.logger = logger;
  }

  public async Task<bool> Send(EmailTemplateType type, Player toPlayer, GameModel gameModel)
  {
    try
    {
      this.logger.LogInformation($"Send type={type}, Player.Email={toPlayer.Email}, Game.Id={gameModel.Game.Id}");

      var templatePath = Path.Combine("EmailTemplates", $"{type}.mt");
      var templateSource = await File.ReadAllTextAsync(templatePath);
      var template = Mustachio.Parser.Parse(templateSource);

      var memberInfos = type.GetType().GetMember(type.ToString());
      var enumValueMemberInfo = memberInfos.FirstOrDefault(m => m.DeclaringType == type.GetType());
      var valueAttributes = enumValueMemberInfo!.GetCustomAttributes(typeof(SubjectAttribute), false);
      var model = this.CreateTemplateModel(toPlayer, gameModel);

      var subject = ((SubjectAttribute)valueAttributes[0]).Text;
      var htmlContent = template(model);
      var plainTextContent = Regex.Replace(htmlContent, @"<[\s\S]*?>[ \t]*[\r\n]?", string.Empty);

      this.logger.LogDebug($"subject --> {subject}");
      this.logger.LogDebug($"htmlContent --> {htmlContent}");
      this.logger.LogDebug($"plainTextContent --> {plainTextContent}");

      if (this.config.EmailEnabled)
      {
        this.logger.LogInformation("Going to send real email");

        return await this.ReallySend(toPlayer, subject, plainTextContent, htmlContent);
      }

      this.logger.LogInformation("Not sending real email");

      return true;
    }
    catch (Exception e)
    {
      this.logger.LogError(e, $"Could not send email: {e.Message}");

      return false;
    }
  }

  private ExpandoObject CreateTemplateModel(Player player, GameModel gameModel)
  {
    dynamic model = new ExpandoObject();

    model.gameId = gameModel.Game.Id;
    model.playerName = player.Name;
    model.opponentName = gameModel.OpponentOf(player).Name;
    model.gameUrl = $"{this.config.BaseUri}/games/{gameModel.Game.Id}";
    model.newGameUrl = $"{this.config.BaseUri}/games/new";
    model.clockRunsOutAt = gameModel.Game.ClockRunsOutAt.SafeFormat("dddd dd MMMM HH:mm");
    model.winnerName = gameModel.Game.WinnerPlayer?.Name ?? string.Empty;
    model.loserPlayer = string.IsNullOrEmpty(model.winnerName) ? string.Empty : gameModel.OpponentOf(gameModel.Game.WinnerPlayer!).Name;

    return model;
  }

  private async Task<bool> ReallySend(Player toPlayer, string subject, string plainTextContent, string htmlContent)
  {
    var client = new MailjetClient(this.config.MailjetApiKey, this.config.MailjetApiSecret);

    var email = new TransactionalEmailBuilder()
      .WithFrom(new SendContact(this.config.ReplyToAddress))
      .WithSubject(subject)
      .WithTextPart(plainTextContent)
      .WithHtmlPart(htmlContent)
      .WithTo(new SendContact(toPlayer.Email))
      .Build();

    try
    {
      var response = await client.SendTransactionalEmailAsync(email);

      this.logger.LogInformation($"Successfully sent email to {toPlayer.Email} ({response.Messages.Length})");

      return true;
    }
    catch (Exception e)
    {
      this.logger.LogError(e, $"Could not send email to {toPlayer.Email}: {e.Message}");

      return false;
    }
  }
}