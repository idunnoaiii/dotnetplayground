
namespace Dotnetplayground.Try.ExceptionHandler;


public class SimpleExceptionHandler<T> : IExceptionHandler where T : Exception
{
    
    public bool HandleException(TryContext context)
    {
        throw new NotImplementedException();
    }
}
