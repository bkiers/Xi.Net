namespace Xi.BlazorApp.Models
{
  public enum EmailTemplateType
  {
    [SubjectAttribute("Xi - A new challenge!")]
    NewGame,

    [SubjectAttribute("Xi - Challenge accepted!")]
    AcceptNewGame,

    [SubjectAttribute("Xi - Challenge declined...")]
    DeclineNewGame,

    [SubjectAttribute("Xi - Time's almost up!")]
    MoveReminder,

    [SubjectAttribute("Xi - you lost the game :(")]
    GameOver,

    [SubjectAttribute("Xi - draw proposal accepted")]
    GameOverDraw,

    [SubjectAttribute("Xi - Oops, time's up!")]
    GameOverTimeUp,

    [SubjectAttribute("Xi - Your turn!")]
    MoveMade,

    [SubjectAttribute("Xi - Draw proposal")]
    DrawProposal,

    [SubjectAttribute("Xi - Draw proposal declined...")]
    DeclineDrawProposal,
  }
}