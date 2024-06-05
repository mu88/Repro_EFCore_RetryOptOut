using Microsoft.EntityFrameworkCore;

namespace Core;

public class MetricsProvider
{
    private readonly IDbContextFactory<MyDbContext> _dbContextFactory;

    public MetricsProvider(IDbContextFactory<MyDbContext> dbContextFactory) => _dbContextFactory = dbContextFactory;

    public async Task<Dictionary<string, int>> GetMetricsAsync()
    {
        MyDbContext dbContext = _dbContextFactory.CreateDbContext();
        int numberOfInteractionPoints;
        try
        {
            // Here I'd like to say: do not use retry strategy
            numberOfInteractionPoints = await dbContext.InteractionPoints.CountAsync();
        }
        catch (Exception)
        {
            numberOfInteractionPoints = 0;
        }

        return new Dictionary<string, int> { { nameof(InteractionPoint), numberOfInteractionPoints } };
    }
}