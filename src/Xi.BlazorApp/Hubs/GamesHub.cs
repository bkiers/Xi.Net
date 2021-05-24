namespace Xi.BlazorApp.Hubs
{
  using System;
  using System.Net.Http;
  using System.Threading.Tasks;
  using Microsoft.AspNetCore.SignalR;
  using Microsoft.AspNetCore.SignalR.Client;
  using Microsoft.Extensions.Logging;

  public enum EventTypes
  {
    MoveMade,
  }

  public class GamesHub : Hub
  {
    public const string HubUrl = "/games-hub";

    private readonly ILogger<GamesHub> logger;

    public GamesHub(ILogger<GamesHub> logger)
    {
      this.logger = logger;
    }

    public static HubConnection Connection(string baseUrl)
    {
      var hubConnection = new HubConnectionBuilder()
        .WithUrl($"{baseUrl.TrimEnd('/')}{HubUrl}", options =>
        {
          options.WebSocketConfiguration = conf =>
          {
            conf.RemoteCertificateValidationCallback = (message, cert, chain, errors) => true;
          };
          options.HttpMessageHandlerFactory = factory => new HttpClientHandler
          {
            ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => true,
          };
        })
        .Build();

      return hubConnection;
    }

    public async Task MoveMade(int gameId)
    {
      await this.Clients.All.SendAsync(EventTypes.MoveMade.ToString(), gameId);
    }

    public override Task OnConnectedAsync()
    {
      this.logger.LogDebug($"{this.Context.ConnectionId} connected");

      return base.OnConnectedAsync();
    }

    public override async Task OnDisconnectedAsync(Exception? e)
    {
      this.logger.LogDebug($"Disconnected {e?.Message} {this.Context.ConnectionId}");

      await base.OnDisconnectedAsync(e);
    }
  }
}