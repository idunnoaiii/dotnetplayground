using Unit = System.ValueTuple;

namespace FP
{
    using System.Collections.Immutable;
    using System.Diagnostics;
    using static F;

    public static partial class F
    {
        public static Unit Unit() => default(Unit);
    }

    public static class Utils
    {
        public static Func<Unit> ToFunc(this Action action)
            => () =>
            {
                action.Invoke();
                return Unit();
            };

        public static Func<T, Unit> ToFunc<T>(this Action<T> action)
           => (t) =>
           {
               action(t);
               return Unit();
           };

        // Map (C<T>, T -> R) -> C<R>
        public static IEnumerable<R> Map<T, R>(this IEnumerable<T> ts, Func<T, R> fn)
        {
            foreach (var t in ts)
                yield return fn(t);
        }

        // Map (C<T>, T -> R) -> C<R>
        public static Option<R> Map<T, R>(this Option<T> opt, Func<T, R> fn)
            => opt.Match<Option<R>>
            (
                () => None,
                (t) => fn(t)
            );

        // ForEach for side effect
        public static IEnumerable<Unit> ForEach<T>(this IEnumerable<T> ts, Action<T> action)
            => ts.Map(action.ToFunc()).ToImmutableList();

        public static Option<Unit> ForEach<T>(this Option<T> opt, Action<T> action)
            => opt.Map(action.ToFunc());
    }

    //Sample code how to use Adapter function
    public static class Timer
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
    }
}