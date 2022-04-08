

using Dotnetplayground.Try;
using Dotnetplayground.Try.WaitingStrategy;

namespace Dotnetplayground.WaitingStrategy;

public class LinearBackoffWaitingStrategy : IWaitingStrategy
{
    private int Timeout;
    private int initialWaitMs;

    public LinearBackoffWaitingStrategy(int ms, int initialWaitMs)
    {
        this.Timeout = ms;
        this.initialWaitMs = initialWaitMs;
    }

    public int ComputeDelay(TryContext context)
    {
        if(context.FailedCount == 1)
            return initialWaitMs;
        return (context.FailedCount - 1) * Timeout;
    }
}
