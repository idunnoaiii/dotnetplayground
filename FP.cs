namespace FP
{
    using static F;

    public static partial class F
    {
        public static Type.None None => Type.None.Default;
        public static Type.Some<T> Some<T>(T value) => new Type.Some<T>(value);

        public static Type.Right<R> Right<R>(R value) => new Type.Right<R>(value);
        public static Type.Left<L> Left<L>(L value) => new Type.Left<L>(value);
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

    public struct Either<L, R>
    {
        readonly bool isRight;
        readonly bool isLeft => !isRight;

        public readonly L LeftValue;
        public readonly R RightValue;

        private Either(L left)
        {
            LeftValue = left;
            isRight = false;
            RightValue = default;
        }

        private Either(R right)
        {
            LeftValue = default;
            isRight = true;
            RightValue = right;
        }

        public static implicit operator Either<L, R>(Type.Left<L> left)
            => new Either<L, R>(left.Value);

        public static implicit operator Either<L, R>(Type.Right<R> right)
            => new Either<L, R>(right.Value);

        public static implicit operator Either<L, R>(L value)
            => new Either<L, R>(value);

        public static implicit operator Either<L, R>(R value)
            => new Either<L, R>(value);

        public RR Match<RR>(Func<L, RR> left, Func<R, RR> right)
            => isRight ? right(RightValue) : left(LeftValue);
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

        public struct Left<L>
        {
            internal L Value { get; }
            internal Left(L value)
            {
                Value = value;
            }
        }

        public struct Right<R>
        {
            internal R Value { get; }
            internal Right(R value)
            {
                Value = value;
            }
        }
    }

}