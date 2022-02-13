using Dotnetplayground.Try.ExceptionHandler;
using Dotnetplayground.Try.RepeatStrategy;
using Dotnetplayground.Try.WaitingStrategy;

namespace Dotnetplayground.Try;

public interface ITry 
{

}

public class Try: ITry
{
    private IList<IExceptionHandler>? exceptionHandlers = new List<IExceptionHandler>();
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
}
