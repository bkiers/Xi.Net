namespace Xi.Models.Exceptions;

using System;

public class InvalidMoveException : Exception
{
  public InvalidMoveException(string message)
    : base(message)
  {
  }
}