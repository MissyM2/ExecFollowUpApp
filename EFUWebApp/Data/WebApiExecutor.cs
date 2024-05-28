namespace EFUWebApp;

public class WebApiExecutor : IWebApiExecutor
{
  private const string apiName = "EFUApi";
  private readonly IHttpClientFactory httpClientFactory;
  public WebApiExecutor(IHttpClientFactory httpClientFactory)
  {
    this.httpClientFactory = httpClientFactory;
  }

  public async Task<T?> InvokeGet<T>(string relativeUrl)
  {
    var httpClient = httpClientFactory.CreateClient(apiName);
    return await httpClient.GetFromJsonAsync<T>(relativeUrl);
  }

}
