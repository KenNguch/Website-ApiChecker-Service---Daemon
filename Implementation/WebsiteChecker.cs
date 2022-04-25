namespace WebsiteChecker.Implementation
{
    internal class WebsiteChecker
    {
        protected readonly ILogger Logger;

        public WebsiteChecker(ILogger logger)
        {
            this.Logger = logger;
        }

        public async Task UrlChecker(string url)
        {
            var client = new HttpClient();
            try
            {
                if (UrlValidity(url).Result)
                {
                    var responseMessage = await client.GetAsync(url);
                    var statusCode = responseMessage.StatusCode;
                    if (responseMessage.IsSuccessStatusCode)
                    {
                        Logger.LogInformation("Website is up.Status Code {StatusCode}", statusCode);
                    }
                    else
                    {
                        Logger.LogError("Website is Down.Status Code {StatusCode} and message is {responseMessage}",
                            statusCode, responseMessage);
                    }
                }
                else
                {
                    Logger.LogError("Invalid Url Passed");
                }
            }
            catch (Exception exception)
            {
                Logger.LogError("This Service has an Error : {error}", exception.Message);
            }
            finally
            {
                client.Dispose();
            }
        }

        protected async Task<bool> UrlValidity(string url)
        {
            try
            {
                var results = Uri.IsWellFormedUriString(url, UriKind.Absolute);
                return await Task.FromResult(results);
            }
            catch (Exception exception)
            {
                Logger.LogError("This Service has an Error : {error}", exception.Message);
                return await Task.FromResult(false);
            }
        }
    }
}