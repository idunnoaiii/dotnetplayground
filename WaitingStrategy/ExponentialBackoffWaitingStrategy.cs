using Dotnetplayground.Try.WaitingStrategy;

namespace Dotnetplayground.Try;

public class ExponentialBackoffWaitingStrategy : IWaitingStrategy
{
    private int Timeout;
    private int initialWaitMs;

    public ExponentialBackoffWaitingStrategy(int ms, int initialWaitMs)
    {
        this.Timeout = ms;
        this.initialWaitMs = initialWaitMs;
    }

    public int ComputeDelay(TryContext context)
    {
        var i = context.FailedCount;
        if (i == 1) return initialWaitMs;
        return i * i * Timeout;
    }
}
