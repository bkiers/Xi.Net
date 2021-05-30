namespace Xi.BlazorApp.Config
{
  public class XiConfig
  {
    public bool EmailEnabled { get; set; }

    public string? SendGridApiKey { get; set; }

    public string? BaseUri { get; set; }

    public string? ReplyToAddress { get; set; }
  }
}