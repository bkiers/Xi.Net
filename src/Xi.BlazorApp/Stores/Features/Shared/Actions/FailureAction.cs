namespace Xi.BlazorApp.Stores.Features.Shared.Actions;

public abstract class FailureAction
{
  protected FailureAction(string errorMessage)
  {
    this.ErrorMessage = errorMessage;
  }

  public string ErrorMessage { get; }
}