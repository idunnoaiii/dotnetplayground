using Dotnetplayground.Try.RepeatStrategy;

namespace Dotnetplayground.Try;

public class NRepeatsWithTimelimitStrategy : IRepeatStrategy
{
    private int n;
    private int maxWaitingTime;

    public NRepeatsWithTimelimitStrategy(int n, int maxWaitingTime)
    {
        this.n = n;
        this.maxWaitingTime = maxWaitingTime;
    }

    public bool ComputeRepeat(TryContext context)
    {
        throw new NotImplementedException();
    }
}
