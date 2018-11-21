using System;
using System.Linq;

namespace Zani.Functional
{
  public static class OptionExtensions
  {
    public static T ValueOrDefault<T>(this Option<T> option, T defaultValue)
    {
      return option.DefaultIfEmpty(defaultValue).First();
    }

    public static TResult Match<T, TResult>(this Option<T> option, Func<T, TResult> some, Func<TResult> none)
    {
      if (some == null)
      {
        throw new ArgumentNullException(nameof(some));
      }

      if (none == null)
      {
        throw new ArgumentNullException(nameof(none));
      }

      return option.Any() ? some(option.First()) : none();
    }

    public static void Match<T>(this Option<T> option, Action<T> some, Action none)
    {
      if (some == null)
      {
        throw new ArgumentNullException(nameof(some));
      }

      if (none == null)
      {
        throw new ArgumentNullException(nameof(none));
      }

      if (option.Any())
      {
        some(option.First());
      }

      none();
    }
  }
}
