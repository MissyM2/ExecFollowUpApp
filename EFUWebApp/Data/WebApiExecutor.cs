using System.Text.Json;
using EFUWebApp.Data;
using Newtonsoft.Json;

namespace EFUWebApp;

public class WebApiExecutor : IWebApiExecutor
{
  private const string apiName = "EFUApi";
  private const string authApiName = "AuthorityApi";
  private readonly IHttpClientFactory httpClientFactory;
  private readonly IConfiguration configuration;
  private readonly IHttpContextAccessor httpContextAccessor;
  public WebApiExecutor(
    IHttpClientFactory httpClientFactory, 
    IConfiguration configuration,
    IHttpContextAccessor httpContextAccessor)
  {
    this.httpClientFactory = httpClientFactory;
    this.configuration = configuration;
    this.httpContextAccessor = httpContextAccessor;
  }

  public async Task<T?> InvokeGet<T>(string relativeUrl)
  {
    var httpClient = httpClientFactory.CreateClient(apiName);
    await AddJwtToHeader(httpClient);
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
    await AddJwtToHeader(httpClient);
    var response = await httpClient.PostAsJsonAsync(relativeUrl, obj);
    
    await HandlePotentialError(response);

    return await response.Content.ReadFromJsonAsync<T>();
  }

  public async Task InvokePut<T>(string relativeUrl, T obj)
  {
    var httpClient = httpClientFactory.CreateClient(apiName);
    await AddJwtToHeader(httpClient);
    var response = await httpClient.PutAsJsonAsync(relativeUrl, obj);
    
    await HandlePotentialError(response);
  }

  public async Task InvokeDelete(string relativeUrl)
  {
    var httpClient = httpClientFactory.CreateClient(apiName);
    await AddJwtToHeader(httpClient);
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

  private async Task AddJwtToHeader(HttpClient httpClient)
  {
    JwtToken? token = null;
    string? strToken = httpContextAccessor.HttpContext?.Session.GetString("access_token");
    if (!string.IsNullOrWhiteSpace(strToken))
    {
      token = JsonConvert.DeserializeObject<JwtToken>(strToken);
    }

    if (token == null)
    {
      var clientId = configuration.GetValue<string>("ClientId");
      var secret = configuration.GetValue<string>("secret");

      // Authenticate against the Authority (need AppCredential class - copied the same class from EFUApi into EFUWebApp)
      var authoClient = httpClientFactory.CreateClient(authApiName);
      var response = await authoClient.PostAsJsonAsync("auth", new AppCredential
      {
        ClientId = clientId,
        Secret=secret

      });

      response.EnsureSuccessStatusCode();

      // Get the JWT from the Authority (need a class that represents the JWT that is returned (access_token and expires_at properties))
      // we currently have the AppCredential class in the EFUApi project.  We need a class that corresponds to that in the EFUWebApp
      strToken = await response.Content.ReadAsStringAsync();
      token = JsonConvert.DeserializeObject<JwtToken>(strToken);

      httpContextAccessor.HttpContext?.Session.SetString("access_token", strToken);
    }
          //  Pass the JWT to endpoints through the http headers
      httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token?.AccessToken);
    
  }

}
