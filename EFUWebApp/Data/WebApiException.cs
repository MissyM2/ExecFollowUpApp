using System.Text.Json;
using EFUWebApp.Data;

namespace EFUWebApp;

public class WebApiException : Exception
{
  public ErrorResponse? ErrorResponse { get;}

  public WebApiException(string errorJson)
  {
    ErrorResponse = JsonSerializer.Deserialize<ErrorResponse>(errorJson);
  }

}
