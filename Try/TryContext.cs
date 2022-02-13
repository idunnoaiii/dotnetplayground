namespace Dotnetplayground.Try;

public class TryContext 
{
    public static implicit operator bool(TryContext context) => context.Succeeded;

    public bool  Succeeded { get; set; }

    public IList<Exception> Exceptions { get; set; }

    public int WaitedTime { get; set; }

    public Try? Try { get; set; }

    public int FailedCount { get; set; }

    public Action<TryContext>? Action { get; set; }

    public void AddException(Exception e) => Exceptions.Add(e); 

    public Exception LastException
    {
        get => Exceptions.Last();
        
    }

    public TryContext() 
    {
        Exceptions = new List<Exception>();
    }

}