using MyCollaborator.Backend.Contexts;
using MyCollaborator.Backend.Services.Interfaces;
using MyCollaborator.Shared.Models;
using Newtonsoft.Json;

namespace MyCollaborator.Backend.Workers;

internal sealed class Synchronizer : BackgroundService
{
    private readonly ILogger<Synchronizer> _logger;
    private readonly IServiceProvider _serviceProvider;

    public Synchronizer(ILogger<Synchronizer> logger, IServiceProvider serviceProvider)
    {
        _logger = logger;
        _serviceProvider = serviceProvider;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            _logger.LogInformation($"Synchronization started running at :{DateTimeOffset.Now}");
            try
            {
                using var scope = _serviceProvider.CreateScope();
                var cachingService = scope.ServiceProvider.GetRequiredService<ICachingService>();
                var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

                var cachedData = await cachingService.GetAllCachedDataAsync();
                var messages = new List<Message>();
                foreach (var cached in cachedData)
                {
                    var message = JsonConvert.DeserializeObject<Message>(cached);
                    messages.Add(message);
                }

                if (messages.Any())
                {
                    await context.Message.AddRangeAsync(messages);
                    await context.SaveChangesAsync();
                }
            }
            catch (Exception e)
            {
                _logger.LogError(new EventId(100), e, "Synchronization failed with error");
            }

            await Task.Delay(TimeSpan.FromDays(7), stoppingToken); //sync database each 8hours to keep every 7 days
        }
    }
}