namespace Xi.BlazorApp.Stores.States
{
  public abstract class RootState
  {
    protected RootState(bool isLoading, string? currentErrorMessage)
    {
      this.IsLoading = isLoading;
      this.CurrentErrorMessage = currentErrorMessage;
    }

    public bool IsLoading { get; }

    public string? CurrentErrorMessage { get; }

    public bool HasCurrentErrors => !string.IsNullOrWhiteSpace(this.CurrentErrorMessage);
  }
}