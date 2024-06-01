using Newtonsoft.Json;

namespace EFUWebApp.Data;

public class JwtToken
{
  // in order to deserialize the JWT, we will need to provide the actual JWT property name, so we decorate with JsonProperty and add NewtonSoft.Json
  [JsonProperty("access_token")]
  public string? AccessToken { get; set; }

  [JsonProperty("expires_at")]
  public DateTime ExpiresAt { get; set; }

}
