using System;
using Xunit;
using Zani.Functional;

namespace Zani.FuncionalTests
{
  public class OptionTests
  {
    [Theory]
    [ClassData(typeof(TestData))]
    public void ExpicitFactoryMethod_SomeValue_ContainsThatValue<T>((T Value, T Default) tuple)
    {
      var option = Option<T>.Some(tuple.Value);
      Assert.Contains(tuple.Value, option);
    }

    [Theory]
    [ClassData(typeof(TestData))]
    public void ImplicitFactoryMethod_SomeValue_ContainsThatValue<T>((T Value, T Default) tuple)
    {
      Option<T> option = tuple.Value;
      Assert.Contains(tuple.Value, option);
    }

    [Fact]
    public void StaticFactoryMethod_NoValue_IsEmpty()
    {
      Option<string> option = Option<string>.None;
      Assert.Empty(option);
    }

    [Fact]
    public void ExpicitFactoryMethod_NullValue_ArgumentNullExceptionIsThrown()
    {
      Assert.Throws<ArgumentNullException>(() => Option<object>.Some(null));
    }

    [Fact]
    public void ImplicitFactoryMethod_NullValue_ArgumentNullExceptionIsThrown()
    {
      Assert.Throws<ArgumentNullException>(() =>
      {
        object value = null;
        Option<object> option = value;
      });
    }

    [Fact]
    public void ImplicitFactoryMethod_Null_OptionIsNull()
    {
      // Implicit operation is not called when null is passed because null doesn't have a type

      Option<object> option = null;
      Assert.Null(option);
    }

    [Fact]
    public void NoneFactory_TwoOptions_ShouldBeEqual()
    {
      Option<string> optionalResource1 = Option<string>.None;
      Option<string> optionalResource2 = Option<string>.None;

      Assert.Equal(optionalResource1, optionalResource2);
    }

    [Theory]
    [ClassData(typeof(TestData))]
    public void ValueOrDefault_SomeValue_ReturnThatValue<T>((T Value, T Default) tuple)
    {
      Option<T> option = tuple.Value;
      T result = option.ValueOrDefault(tuple.Default);

      Assert.Equal(tuple.Value, result);
    }

    [Theory]
    [ClassData(typeof(TestData))]
    public void ValueOrDefault_NoValue_ReturnDefaultValue<T>((T Value, T Default) tuple)
    {
      Option<T> option = Option<T>.None;
      T result = option.ValueOrDefault(tuple.Default);

      Assert.Equal(tuple.Default, result);
    }

    [Fact]
    public void ValueOrDefault_NoValueAndNullAsDefault_ReturnNull()
    {
      // I'll let the caller use anything for defaultValue, including null

      Option<object> option = Option<object>.None;
      object defaultValue = null;
      object result = option.ValueOrDefault(defaultValue);

      Assert.Equal(defaultValue, result);
    }

    [Fact]
    public void MatchAction_SomeValue_SomeFuncIsCalled()
    {
      Option<object> option = new object();

      bool someWasCalled = false;
      option.Match(
        some: arg => { someWasCalled = true; },
        none: () => { });

      Assert.True(someWasCalled);
    }

    [Fact]
    public void MatchAction_WithNoValue_NoneFuncIsCalled()
    {
      var option = Option<object>.None;

      bool noneWasCalled = false;
      object result = option.Match(
        some: arg => arg,
        none: () => 
        {
          noneWasCalled = true;
          return new object();
        });

      Assert.True(noneWasCalled);
    }

    [Fact]
    public void MatchFunc_WithSomeValue_SomeFuncIsCalled()
    {
      Option<object> option = new object();

      bool someWasCalled = false;
      object result = option.Match(
        some: arg => {
          someWasCalled = true;
          return arg;
          },
        none: () => new object());

      Assert.True(someWasCalled);
    }

    [Theory]
    [ClassData(typeof(TestData))]
    public void MatchFunc_WithSomeValue_ThatValueIsReturned<T>((T Value, T Default) tuple)
    {
      Option<T> option = tuple.Value;

      T result = option.Match(
        some: arg => arg,
        none: () => tuple.Default);

      Assert.Equal(option, result);
    }

    [Fact]
    public void MatchFunc_WithNoValue_NoneFuncIsCalled()
    {
      var option = Option<object>.None;

      bool noneWasCalled = false;
      object result = option.Match(
        some: arg => arg,
        none: () =>
        {
          noneWasCalled = true;
          return new object();
        });

      Assert.True(noneWasCalled);
    }

    [Theory]
    [ClassData(typeof(TestData))]
    public void MatchFunc_WithNoValue_DefaultValueIsReturned<T>((T Value, T Default) tuple)
    {
      var option = Option<T>.None;

      T result = option.Match(
        some: arg => arg,
        none: () => tuple.Default);

      Assert.Equal(tuple.Default, result);
    }

    [Fact]
    public void Match_SomeActionIsNull_ArgumentNullExceptionIsThrown()
    {
      var option = Option<object>.None;
      Assert.Throws<ArgumentNullException>(() => 
        option.Match(
          some: null,
          none: () => { }));
    }

    [Fact]
    public void Match_NoneActionIsNull_ArgumentNullExceptionIsThrown()
    {
      var option = Option<object>.None;
      Assert.Throws<ArgumentNullException>(() =>
        option.Match(
          some: arg => { },
          none: null));
    }

    [Fact]
    public void Match_SomeFuncIsNull_ArgumentNullExceptionIsThrown()
    {
      var option = Option<object>.None;
      Assert.Throws<ArgumentNullException>(() => 
        option.Match(
          some: null,
          none: () => new object()));
    }

    [Fact]
    public void Match_NoneFuncIsNull_ArgumentNullExceptionIsThrown()
    {
      var option = Option<object>.None;
      Assert.Throws<ArgumentNullException>(() => 
        option.Match(
          some: arg => arg,
          none: null));
    }
  }
}
