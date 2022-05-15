namespace FP
{
    using static F;

    public static partial class F
    {
        public static Type.None None => Type.None.Default;
        public static Type.Some<T> Some<T>(T value) => new Type.Some<T>(value);
    }

    public struct Option<T>
    {
        readonly bool isSome;

        //public here to faciliate my Dump function
        public readonly T value;

        private Option(T value)
        {
            this.isSome = true;
            this.value = value;
        }

        public static implicit operator Option<T>(Type.None _) =>
            new Option<T>();

        public static implicit operator Option<T>(Type.Some<T> some) =>
            new Option<T>(some.Value);

        public static implicit operator Option<T>(T value) =>
            value == null ? None : Some(value);

        public R Match<R>(Func<R> none, Func<T, R> some) =>
            isSome ? some(value) : none();

        public IEnumerable<T> AsEnumerable()
        {
            if (isSome) yield return value;
        }
    }

    namespace Type
    {
        public struct None
        {
            internal static readonly None Default = new None();
        }

        public struct Some<T>
        {
            internal T Value { get; }
            internal Some(T value)
            {
                if (value == null)
                    throw new ArgumentNullException();
                Value = value;
            }
        }
    }

}