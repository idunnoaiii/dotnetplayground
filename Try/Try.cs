using Dotnetplayground.Try.ExceptionHandler;
using Dotnetplayground.Try.RepeatStrategy;
using Dotnetplayground.Try.WaitingStrategy;

namespace Dotnetplayground.Try;

public interface ITry 
{

}

public class Try: ITry
{
    private IList<IExceptionHandler> exceptionHandlers = new List<IExceptionHandler>();
    public IWaitingStrategy? WaitingStrategy { get; set; }
    public IRepeatStrategy? RepeatStrategy { get; set; }

    public bool FailQueitly { get; set; }

    public Try()
    {
        this.Repeat(4)
            .BeQuite(false)
            .WithWaitingStrategy(new ExponentialBackoffWaitingStrategy(50, 0));
    }

    public void AddExceptionHandler(IExceptionHandler handler)
    {
        this.exceptionHandlers?.Add(handler);
    }

    private void ExecuteAction(TryContext context)
    {
        context.Action?.Invoke(context);
        context.Succeeded = true;
    }

    public static Try Default => new Try()
    .Expect<Exception>()
    .Repeat(4)
    .BeQuite(false)
    .WithWaitingStrategy(new ExponentialBackoffWaitingStrategy(50, 0));

    public TryContext Execute(Action<TryContext> action)
    {
        ArgumentNullException.ThrowIfNull(RepeatStrategy);
        ArgumentNullException.ThrowIfNull(WaitingStrategy);

        var tryContext = new TryContext
        {
            Action = action,
            FailedCount = 0,
            Try = this
        };

        while(RepeatStrategy.ComputeRepeat(tryContext))
        {
            try 
            {
                ExecuteAction(tryContext);
                return tryContext;
            }
            catch(Exception e)
            {
                tryContext.FailedCount ++;
                tryContext.AddException(e);

                bool handled = false;
                foreach(var handler in exceptionHandlers)
                {
                    handled |= handler.HandleException(tryContext);
                }

                if(!handled)
                {
                    if(FailQueitly)
                    {
                        break;
                    }
                    throw e;
                }

            }

            if(!RepeatStrategy.ComputeRepeat(tryContext)) break;

            var wait = WaitingStrategy.ComputeDelay(tryContext);
            Thread.Sleep(wait);
            tryContext.WaitedTime += wait;
        }

        if(!FailQueitly && !tryContext)
        {
            throw new AggregateException("failed to execute", tryContext.Exceptions);
        }

        return tryContext;
    }
}
