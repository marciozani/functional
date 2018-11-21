using System;
using System.Collections.Generic;
using Xunit;

namespace Zani.FuncionalTests
{
  public class TestData : TheoryData<object>
  {
    public TestData()
    {
      Add((string.Empty, "empty string"));
      Add(("resource", "resource not found"));
      Add((0, -1));
      Add((123, 456));
      Add((true, false));
      Add((DateTime.MinValue, DateTime.MaxValue));
      Add((new object(), new object()));
      Add((new int[] { 1, 2, 3 }, new int[] { 4, 5, 6 }));
      Add((new List<string>(), new List<string>()));
    }
  }
}
