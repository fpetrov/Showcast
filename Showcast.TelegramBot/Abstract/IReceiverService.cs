namespace Showcast.TelegramBot.Abstract;

public interface IReceiverService
{
    public Task ReceiveAsync(CancellationToken stoppingToken);
}