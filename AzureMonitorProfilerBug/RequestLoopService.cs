namespace AzureMonitorProfilerBug;

public class RequestLoopService(HttpClient client, ILogger<RequestLoopService> logger) : BackgroundService
{
    private static string BaseUrl => Environment.GetEnvironmentVariable("DOTNET_RUNNING_IN_CONTAINER") == "true"
        ? "http://localhost:8080/"
        : "http://localhost:5139/";

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (true)
        {
            if (stoppingToken.IsCancellationRequested)
                break;
            try
            {
                await client.GetAsync($"{BaseUrl}Request/Noop", stoppingToken); //Succeeds
                await client.GetAsync($"{BaseUrl}Request/HttpClient", stoppingToken); //Fails
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