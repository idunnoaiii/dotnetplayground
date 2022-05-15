using System.Diagnostics;
using DotnetPlayGround;
using FP;
using static FP.Helper.FPHelper;
using static FP.F;
using Unit = System.ValueTuple;
using static System.Console;

using Pet = System.String;

var neighbors = new[]
{
 new { Name = "John", Pets = new Pet[] {"Fluffy", "Thor"} },
 new { Name = "Tim", Pets = new Pet[] {} },
 new { Name = "Carl", Pets = new Pet[] {"Sybil"} },
};

var nested = neighbors.Map(n => n.Pets);

nested.Dump();
var flat = neighbors.Bind(n => n.Pets);
flat.Dump();

/**
Age ReadAge()
    => ParseAge(Prompt("Please enter your age"))
        .Match
        (
            () => ReadAge(),
            (age) => age
        );

Option<Age> ParseAge(string input)
    => ParseInt(input).Bind(Age.Of);

string Prompt(string prompt)
{
    WriteLine(prompt);
    return ReadLine();
}

ReadAge();
*/
