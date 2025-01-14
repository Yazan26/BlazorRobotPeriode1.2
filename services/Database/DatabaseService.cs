
using DependencyInjectionExample.Data;


public class DatabaseService : IDatabaseService
{
    private readonly IServiceScopeFactory _serviceScopeFactory;

    public DatabaseService(IServiceScopeFactory serviceScopeFactory)
    {
        _serviceScopeFactory = serviceScopeFactory;
    }

    public async Task SaveBatteryPercentage(int percentage)
    {
        using var scope = _serviceScopeFactory.CreateScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
        await dbContext.BatteryPercentages.AddAsync(new BatteryPercentage { Percentage = percentage, Timestamp = DateTime.UtcNow });
        await dbContext.SaveChangesAsync();
    }

    public async Task SaveObjectCount(int count)
    {
        using var scope = _serviceScopeFactory.CreateScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
        await dbContext.ObjectCounts.AddAsync(new ObjectCount { Count = count, Timestamp = DateTime.UtcNow });
        await dbContext.SaveChangesAsync();
    }

    public async Task SaveDistance(int distance)
    {
        using var scope = _serviceScopeFactory.CreateScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
        await dbContext.Distances.AddAsync(new Distance { Value = distance, Timestamp = DateTime.UtcNow });
        await dbContext.SaveChangesAsync();
    }
}