using Unit = System.ValueTuple;

namespace FP;

using System.Collections.Immutable;
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
    public static IEnumerable<R> Map<T, R>
    (this IEnumerable<T> ts, Func<T, R> fn)
    {
        foreach (var t in ts)
            yield return fn(t);
    }

    // Map (C<T>, T -> R) -> C<R>
    public static Option<R> Map<T, R>
        (this Option<T> opt, Func<T, R> fn)
        => opt.Match<Option<R>>
        (
            () => None,
            (t) => fn(t)
        );

    // ForEach for side effect
    public static IEnumerable<Unit> ForEach<T>
        (this IEnumerable<T> ts, Action<T> action)
        => ts.Map(action.ToFunc()).ToImmutableList();


    public static Option<Unit> ForEach<T>
        (this Option<T> opt, Action<T> action)
        => opt.Map(action.ToFunc());


    public static Option<R> Bind<T, R>
        (this Option<T> optT, Func<T, Option<R>> fn)
        => optT.Match<Option<R>>
        (
            () => None,
            (t) => fn(t)
        );

    public static IEnumerable<R> Bind<T, R>
    (this IEnumerable<T> ts, Func<T, IEnumerable<R>> f)
    {
        foreach (T t in ts)
            foreach (R r in f(t))
                yield return r;
    }

    public static Option<T> Where<T>
        (this Option<T> opt, Func<T, bool> predicate)
        => opt.Match
        (
            () => None,
            (t) => predicate(t) ? opt : None
        );


    public static IEnumerable<R> Bind<T, R>
        (this IEnumerable<T> list, Func<T, Option<R>> func)
        => list.Bind(t => func(t).AsEnumerable());

    public static IEnumerable<R> Bind<T, R>
        (this Option<T> opt, Func<T, IEnumerable<R>> func)
        => opt.AsEnumerable().Bind(func);
}



