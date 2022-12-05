namespace ConsoleStreaming
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            await BeginReadText();
        }

        static async Task BeginReadText()
        {
            var http = new HttpClient()
            {
                BaseAddress = new Uri("http://localhost:5125"),
            };

            using var request = new HttpRequestMessage(HttpMethod.Get, "DataStreaming");
            using var response = await http.SendAsync(request, HttpCompletionOption.ResponseHeadersRead);
            var responseStream = await response.Content.ReadAsStreamAsync();

            using (var sr = new StreamReader(responseStream, bufferSize: 128)) // 128: Minimum value for StreamReader
            {
                string? line;
                while ((line = await sr.ReadLineAsync()) != null) // Not passing the cancellation token to be .Net 6 compatible. Passing the token has no effect on the issue.
                {
                    Console.WriteLine(line);
                }
            }
        }
    }
}