using Dotnetplayground.Try.RepeatStrategy;

namespace Dotnetplayground.Try;


public class NRepeatsTryRepeatStrategy: IRepeatStrategy
{
    private int N { get; set;}

    public NRepeatsTryRepeatStrategy(int n)
    {
        this.N = n;
    }

    public bool ComputeRepeat(TryContext context)
    {
        throw new NotImplementedException();
    }
}
