using FP;
using static FP.F;

namespace DotnetPlayGround;

public struct Age
{
    private int Value { get; }

    public static Option<Age> Of(int value)
        => IsValid(value) ? Some(new Age(value)) : None;

    private Age(int value)
    {
        if (!IsValid(value))
            throw new ArgumentException($"{value}  is not a valid age");

        Value = value;
    }

    private static bool IsValid(int age)
        => 0 <= age && age < 120;
}
