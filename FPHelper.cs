using System.Diagnostics;
using Unit = System.ValueTuple;
using static FP.F;

namespace FP.Helper;

public static class FPHelper
{
    public static T Time<T>(string op, Func<T> fn)
    {
        var sw = new Stopwatch();
        sw.Start();
        T t = fn();
        sw.Stop();
        Console.WriteLine($"{op} took {sw.ElapsedMilliseconds}ms");
        return t;
    }

    public static Unit Time(string op, Action afn)
    {
        return Time<Unit>(op, afn.ToFunc());
    }

    public static Option<int> ParseInt(string s)
    {
        return int.TryParse(s, out var number) ? Some(number) : None;
    }
}
