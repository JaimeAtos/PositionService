using Domain.Helpers;

namespace Shared.Services;

public class DateTimeService : IDateTimeService
{
   public DateTime GetDateTime => DateTime.UtcNow;
}
