using Dotnetplayground.Try.ExceptionHandler;
using Dotnetplayground.Try.WaitingStrategy;

namespace Dotnetplayground.Try;

public static class TryExtension 
{
    public static Try BackOfExponentially(this Try self, int timeout = 10, int initialTimeout = 0)
    {

        return self;
    }

    public static Try BackOfLinearly(this Try self, int timeout = 10, int initialTimeout = 0)
    {
        
        return self;
    }

    public static Try WaitConstantAmount(this Try self, int timeout = 10, int initialTimeout =0)
    {

        return self;
    }

    public static Try WithWaitingStrategy(this Try self, IWaitingStrategy strategy)
    {
        self.WaitingStrategy = strategy;
        return self;
    }

    public static Try HandleException(this Try self, IExceptionHandler handler)
    {
      self.AddExceptionHandler(handler);
      return self;
    }

    public static Try Repeat(this Try self, int n)
    {
      var strategy = new NRepeatsTryRepeatStrategy(n);
      self.RepeatStrategy = strategy;
      return self;
    }

    public static Try Repeat(this Try self, int n, int maxWaitingTime)
    {
        var strategy = new NRepeatsWithTimelimitStrategy(n, maxWaitingTime);
        self.RepeatStrategy = strategy;
        return self;
    }

    public static Try Expect<T>(this Try self) where T : Exception
    {
        self.HandleException(new SimpleExceptionHandler<T>());
        return self;
    }

    public static Try Expect<T>(this Try self, Action<T> exceptionHandler) where T : Exception
    {
        self.HandleException(new DelegateExceptionHandler<T>(exceptionHandler));
        return self;
    }

    public static Try BeQuite(this Try self, bool quite)
    {
        self.FailQueitly = quite;
        return self;
    }

    public static TResult Execute<TResult>(this Try self, Func<TResult> action)
    {
        return self.Execute<TResult>(context => action.Invoke());
    }

    public static TResult Execute<TResult>(this Try self, Func<TryContext, TResult> action)
    {
        TResult? result = default(TResult);
        self
        .BeQuite(false)
        .Execute(context => 
        {
            result = action.Invoke(context);
        });
        return result!;
    }

    public static TryContext Execute(this Try self, Action action)
    {
        return self.Execute(context => action.Invoke());
    }
}