namespace Xi.BlazorApp.Pages;

using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
[IgnoreAntiforgeryToken]
public class Error : PageModel
{
  public string? RequestId { get; set; } = default!;

  public bool ShowRequestId => !string.IsNullOrEmpty(this.RequestId);

  public void OnGet()
  {
    this.RequestId = Activity.Current?.Id ?? this.HttpContext.TraceIdentifier;
  }
}