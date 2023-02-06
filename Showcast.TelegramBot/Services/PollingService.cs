using Microsoft.Extensions.Logging;
using Showcast.TelegramBot.Abstract;

namespace Showcast.TelegramBot.Services;

// Compose Polling and ReceiverService implementations
public class PollingService : PollingServiceBase<ReceiverService>
{
    public PollingService(IServiceProvider serviceProvider, ILogger<PollingService> logger)
        : base(serviceProvider, logger)
    {
        
    }
}
