namespace Xi.BlazorApp.Shared;

using System;
using Microsoft.AspNetCore.Components;

public partial class ToggleComponent
{
  [Parameter]
  public string Text { get; set; } = string.Empty;

  [Parameter]
  public string? ToolTip { get; set; }

  [Parameter]
  public bool Value { get; set; }

  [Parameter]
  public Action<bool>? ValueChanged { get; set; }

  [Parameter]
  public EventCallback<bool> OnToggled { get; set; }

  private void Toggle(bool value)
  {
    if (this.Value == value)
    {
      // Only trigger a change if the value actually changed.
      return;
    }

    this.Value = value;

    // If there's an `@bind-Value` assigned, invoke it.
    this.ValueChanged?.Invoke(value);

    if (this.OnToggled.HasDelegate)
    {
      // OnToggled="@..." was set: invoke it.
      this.OnToggled.InvokeAsync(this.Value);
    }
  }
}