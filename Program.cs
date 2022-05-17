using System.Diagnostics;
using DotnetPlayGround;
using FP;
using static FP.Helper.FPHelper;
using static FP.F;
using Unit = System.ValueTuple;
using static System.Console;

var TransferValidator = new TransferValidator<MakeTransfer>();

void MakeTransfer(MakeTransfer transfer)
    => ((Option<MakeTransfer>) Some(transfer))
        .Map(Normalize)
        .Where(TransferValidator.IsValid)
        .ForEach(Book);

MakeTransfer(new MakeTransfer(10));


MakeTransfer Normalize(MakeTransfer transfer) 
    => transfer with { balance = 2};

void Book(MakeTransfer transfer)
{
    transfer.balance.Dump();
}

interface IValidator<T> 
{
    bool IsValid(T t);
}

class TransferValidator<MakeTransfer> : IValidator<MakeTransfer>
{
    public bool IsValid(MakeTransfer t)
    {
        return false;
    }
}

record MakeTransfer(int balance);