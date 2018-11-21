using System;
using System.Collections;
using System.Collections.Generic;

namespace Zani.Functional
{
  public class Option<T> : IEnumerable<T>
  {
    private readonly T[] _values;

    private Option(T[] values)
    {
      _values = values;
    }

    public static Option<T> Some(T value)
    {
      if (value == null)
        throw new ArgumentNullException(nameof(value));

      return new Option<T>(new T[] { value });
    }

    public static implicit operator Option<T>(T value) =>
      Some(value);

    public static Option<T> None { get; } = new Option<T>(new T[0]);

    public IEnumerator<T> GetEnumerator() => ((IEnumerable<T>)_values).GetEnumerator();

    IEnumerator IEnumerable.GetEnumerator() => _values.GetEnumerator();
  }
}
