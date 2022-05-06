using static Old.Functional.F;
using DotnetPlayGround;
using Old.Functional;

int showAge(Option<int> age)
    => age.Match
    (
        () => 0,
        (value) => value
    );

showAge(None).Dump();