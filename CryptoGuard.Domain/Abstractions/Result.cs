namespace CryptoGuard.Domain.Abstractions;

public class Result<T>
{
    public T? Value { get; }
    public Error? Error { get; }
    public bool IsSuccess => Error == null;
    
    private Result(T? value, Error? error)
    {
        if (error != null && !EqualityComparer<T>.Default.Equals(value, default!))
        {
            throw new InvalidOperationException("A result cannot have both a value and an error.");
        }
        
        Value = value;
        Error = error;
    }
    
    public static Result<T> Success(T value) => new(value, null);
    public static implicit operator Result<T>(FailureResult failure) => Failure(failure.Error);
    private static Result<T> Failure(Error error) => new(default, error);
}

public static class Result
{
    public static Result<T> Success<T>(T value) => Result<T>.Success(value);
    public static FailureResult Failure(Error error) => new FailureResult(error);
}

public record FailureResult(Error Error);

public readonly struct Unit
{
    public static readonly Unit Value = default;
}
