using Microsoft.AspNetCore.Mvc;

namespace BlazorStreaming.Server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class DataStreamingController : ControllerBase
    {
        [HttpGet]
        public async Task Get(CancellationToken cancellation)
        {
            Response.ContentType = "text/plain";
            StreamWriter sw;
            await using ((sw = new StreamWriter(Response.Body)).ConfigureAwait(false))
            {
                int counter = 0;
                while (true)
                {
                    await sw.WriteLineAsync(counter.ToString("00000000000000000000"));
                    await sw.FlushAsync();
                    //if (counter >= 10) break;
                    await Task.Delay(1000, cancellation);
                    counter++;
                }
            }
        }
    }
}
