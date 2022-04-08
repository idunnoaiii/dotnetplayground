using Dotnetplayground.Try.WaitingStrategy;

namespace Dotnetplayground.Try;

public class ConstantBackoffWaitingStrategy : IWaitingStrategy
{
    public int TimeOut { get; set; }
    public int InitTimeOut { get; set; }
    
    public ConstantBackoffWaitingStrategy(int timeOut, int initTimeOut)
    {
        this.TimeOut = timeOut;
        this.InitTimeOut = initTimeOut;    
    }

    public int ComputeDelay(TryContext context)
    {
        if(context.FailedCount == 1)
            return InitTimeOut;
        return TimeOut;
    }
}