namespace Xi.BlazorApp.Models
{
  public enum EmailTemplateType
  {
    [SubjectAttribute("Time's almost up!")]
    MoveReminder,

    [SubjectAttribute("Oops, time's up!")]
    TimeRanOut,

    // [SubjectAttribute("Oops, time's up!")]
    // TimeRanOut,
  }
}