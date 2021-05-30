namespace Xi.BlazorApp.Models
{
  using System;

  [AttributeUsage(AttributeTargets.Field)]
  public class SubjectAttribute : Attribute
  {
    public SubjectAttribute(string text)
    {
      this.Text = text;
    }

    public string Text { get; }
  }
}