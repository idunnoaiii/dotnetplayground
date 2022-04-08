using Dotnetplayground.Try.RepeatStrategy;

namespace Dotnetplayground.Try;

public class NRepeatsWithTimelimitStrategy : IRepeatStrategy
{
    private int N;
    private int maxWaitingTime;

    public NRepeatsWithTimelimitStrategy(int n, int maxWaitingTime)
    {
        this.N = n;
        this.maxWaitingTime = maxWaitingTime;
    }

    public bool ComputeRepeat(TryContext context)
    {
        if(context.WaitedTime >= maxWaitingTime)
            return false;
        return context.FailedCount < N;
    }
}
