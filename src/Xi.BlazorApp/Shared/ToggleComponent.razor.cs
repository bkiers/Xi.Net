namespace Xi.BlazorApp.Shared
{
  using System;
  using Microsoft.AspNetCore.Components;

  public partial class ToggleComponent
  {
    [Parameter]
    public bool Value { get; set; }

    [Parameter]
    public Action<bool> ValueChanged { get; set; } = default!;

    [Parameter]
    public EventCallback<bool> OnToggled { get; set; }

    private void Toggle(bool value)
    {
      if (this.Value == value)
      {
        // Only trigger a change if the value really changed.
        return;
      }

      this.Value = value;
      this.ValueChanged.Invoke(value);

      if (this.OnToggled.HasDelegate)
      {
        this.OnToggled.InvokeAsync(this.Value);
      }
    }
  }
}