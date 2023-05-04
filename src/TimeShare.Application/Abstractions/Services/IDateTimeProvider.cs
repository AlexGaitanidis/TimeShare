namespace TimeShare.Application.Abstractions.Services;

public interface IDateTimeProvider
{
    DateTime UtcNow { get; }
}