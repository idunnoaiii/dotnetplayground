namespace Dotnetplayground.Try.RepeatStrategy;


public interface IRepeatStrategy 
{
    bool ComputeRepeat(TryContext context);
}