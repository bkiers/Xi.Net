namespace Xi.BlazorApp.Extensions
{
  using System.Threading.Tasks;
  using Microsoft.AspNetCore.SignalR.Client;

  public static class HubConnectionExtensions
  {
    public static async Task StartSendStopAsync(this HubConnection hubConnection, string method, object arg)
    {
      await hubConnection.StartAsync();
      await hubConnection.SendAsync(method, arg);
      await hubConnection.StopAsync();
    }
  }
}