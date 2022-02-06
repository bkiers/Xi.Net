namespace Xi.BlazorApp.Stores.States;

public abstract class RootState
{
  protected RootState(bool isLoading, string? errorMessage)
  {
    this.IsLoading = isLoading;
    this.ErrorMessage = errorMessage;
  }

  public bool IsLoading { get; }

  public string? ErrorMessage { get; }

  public bool HasErrors => !string.IsNullOrWhiteSpace(this.ErrorMessage);
}