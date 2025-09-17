using AutoMapper;
using GroceryManager.Database;
using GroceryManager.Database.Entities;
using GroceryManager.Models.Dtos.Revenue;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace GroceryManager.Services.Revenues
{
    public class RevenueSyncBackgroundService : BackgroundService
    {
        private readonly ILogger<RevenueSyncBackgroundService> _logger;
        private readonly IServiceProvider _serviceProvider;
        private readonly IMapper _mapper;

        public RevenueSyncBackgroundService(
            ILogger<RevenueSyncBackgroundService> logger,
            IServiceProvider serviceProvider,
            IMapper mapper)
        {
            _logger = logger;
            _serviceProvider = serviceProvider;
            _mapper = mapper;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation("RevenueSyncBackgroundService iniciado.");

            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    using var scope = _serviceProvider.CreateScope();
                    var revenuesService = scope.ServiceProvider.GetRequiredService<IRevenuesService>();
                    var context = scope.ServiceProvider.GetRequiredService<DataContext>();

                    var revenues = await revenuesService.GetAllRevenuesApiAsync();

                    var existingIds = context.Revenues.Select(r => r.ApiId).ToHashSet();
                    var newRevenues = revenues.Where(r => !existingIds.Contains(r.ApiId)).ToList();

                    if (newRevenues.Any())
                    {
                        var revenueEntities = _mapper.Map<List<Revenue>>(newRevenues);
                        context.Revenues.AddRange(revenueEntities);
                        await context.SaveChangesAsync();
                    }

                    _logger.LogInformation("New recipes added: {Count}", newRevenues.Count);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error syncing recipes.");
                }

                await Task.Delay(TimeSpan.FromDays(1), stoppingToken);
            }
        }
    }
}