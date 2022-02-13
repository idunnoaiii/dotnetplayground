namespace Dotnetplayground.Try.WaitingStrategy;


public interface IWaitingStrategy 
{
    int ComputeDelay(TryContext context);
}