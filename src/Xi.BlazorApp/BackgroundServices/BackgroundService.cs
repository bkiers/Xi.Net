namespace Xi.BlazorApp.BackgroundServices;

using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;

public abstract class BackgroundService : IHostedService, IDisposable
{
  private readonly CancellationTokenSource stoppingTokenSource = new();
  private Task? executingTask;

  public virtual Task StartAsync(CancellationToken cancellationToken)
  {
    // Store the task we're executing
    this.executingTask = this.ExecuteAsync(this.stoppingTokenSource.Token);

    // If the task is completed then return it,
    // this will bubble cancellation and failure to the caller
    if (this.executingTask.IsCompleted)
    {
      return this.executingTask;
    }

    // Otherwise it's running
    return Task.CompletedTask;
  }

  public virtual async Task StopAsync(CancellationToken cancellationToken)
  {
    // Stop called without start
    if (this.executingTask == null)
    {
      return;
    }

    try
    {
      // Signal cancellation to the executing method
      this.stoppingTokenSource.Cancel();
    }
    finally
    {
      // Wait until the task completes or the stop token triggers
      await Task.WhenAny(this.executingTask, Task.Delay(Timeout.Infinite, cancellationToken));
    }
  }

  public virtual void Dispose()
  {
    this.stoppingTokenSource.Cancel();
  }

  protected abstract Task ExecuteAsync(CancellationToken stoppingToken);
}