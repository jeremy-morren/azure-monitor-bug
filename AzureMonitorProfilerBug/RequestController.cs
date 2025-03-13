using Microsoft.AspNetCore.Mvc;

namespace AzureMonitorProfilerBug;

[ApiController, Route("[controller]/[action]")]
public class RequestController(HttpClient client) : ControllerBase
{
    [HttpGet]
    public void Noop() {}

    [HttpGet]
    public async Task HttpClient() => await client.GetAsync("https://api.github.com");
}