namespace Dotnetplayground.Try.ExceptionHandler;

public interface IExceptionHandler 
{
    bool HandleException(TryContext context);
}