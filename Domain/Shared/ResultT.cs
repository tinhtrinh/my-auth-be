using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Domain.Shared;

public class Result<TValue> : Result
{
    private readonly TValue? _value;

    protected internal Result(TValue? value, bool isSucess, Error error)
        : base(isSucess, error) => _value = value;

    public TValue Value => IsSuccess ? _value! : throw new InvalidOperationException("The value of a failure result can not be accessed.");
}