using Dotnetplayground.Try.WaitingStrategy;

namespace Dotnetplayground.Try;

public class ExponentialBackoffWaitingStrategy : IWaitingStrategy
{
    private int v1;
    private int v2;

    public ExponentialBackoffWaitingStrategy(int v1, int v2)
    {
        this.v1 = v1;
        this.v2 = v2;
    }

    public int ComputeDelay(TryContext context)
    {
        throw new NotImplementedException();
    }
}
