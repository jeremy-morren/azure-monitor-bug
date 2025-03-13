namespace AzureMonitorProfilerBug;

public class RequestLoopService(HttpClient client, ILogger<RequestLoopService> logger) : BackgroundService
{
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (true)
        {
            if (stoppingToken.IsCancellationRequested)
                break;
            try
            {
                await client.GetAsync("http://localhost:5139/Request/Noop", stoppingToken); //Succeeds
                await client.GetAsync("http://localhost:5139/Request/HttpClient", stoppingToken); //Fails
                await Task.Delay(1000, stoppingToken);
            }
            catch (OperationCanceledException ex) when (ex.CancellationToken == stoppingToken)
            {
                break;
            }
            catch (Exception ex)
            {
                //Ignore
                logger.LogError(ex, "Error in RequestLoopService");
            }
        }
    }
}