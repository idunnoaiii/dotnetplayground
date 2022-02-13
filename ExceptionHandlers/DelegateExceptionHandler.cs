namespace Dotnetplayground.Try.ExceptionHandler;

public class DelegateExceptionHandler<T> : IExceptionHandler where T : Exception
{
    private Action<T> exceptionHandler;

    public DelegateExceptionHandler(Action<T> exceptionHandler)
    {
        this.exceptionHandler = exceptionHandler;
    }

    public bool HandleException(TryContext context)
    {
        throw new NotImplementedException();
    }
}
