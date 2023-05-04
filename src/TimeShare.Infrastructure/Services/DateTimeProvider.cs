using TimeShare.Application.Abstractions.Services;

namespace TimeShare.Infrastructure.Services;

public class DateTimeProvider : IDateTimeProvider
{
    public DateTime UtcNow => DateTime.UtcNow;
}