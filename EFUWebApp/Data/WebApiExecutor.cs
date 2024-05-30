using System.Text.Json;
using EFUWebApp.Data;

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
    // for exception handling, we do not have a response to work with
    // so, we are commenting the next line (return)
    // and using another method, so we can get a response
    //return await httpClient.GetFromJsonAsync<T>(relativeUrl);
    var request = new HttpRequestMessage(HttpMethod.Get, relativeUrl);
    var response = await httpClient.SendAsync(request);

    // by substituting the above 2 lines, we get a response and now can call our 
    // exception handling method
    await HandlePotentialError(response);

    return await response.Content.ReadFromJsonAsync<T>();

  }

  public async Task<T?> InvokePost<T>(string relativeUrl, T obj)
  {
    var httpClient = httpClientFactory.CreateClient(apiName);
    var response = await httpClient.PostAsJsonAsync(relativeUrl, obj);
    
    await HandlePotentialError(response);

    return await response.Content.ReadFromJsonAsync<T>();
  }

  public async Task InvokePut<T>(string relativeUrl, T obj)
  {
    var httpClient = httpClientFactory.CreateClient(apiName);
    var response = await httpClient.PutAsJsonAsync(relativeUrl, obj);
    
    await HandlePotentialError(response);
  }

  public async Task InvokeDelete(string relativeUrl)
  {
    var httpClient = httpClientFactory.CreateClient(apiName);
    var response = await httpClient.DeleteAsync(relativeUrl);
    
    await HandlePotentialError(response);
  }

  private async Task HandlePotentialError(HttpResponseMessage httpResponse)
  {
    if (!httpResponse.IsSuccessStatusCode)
    {
      var errorJson = await httpResponse.Content.ReadAsStringAsync();
      throw new WebApiException(errorJson);
    }

  }

}
