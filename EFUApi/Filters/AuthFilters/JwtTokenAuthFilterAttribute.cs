using EFUApi.Authority;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace EFUApi.Filters.AuthFilters;

// this filter is the authorization pipeline.  The request hits this filter BEFORE it hits the Action Method
public class JwtTokenAuthFilterAttribute : Attribute, IAsyncAuthorizationFilter
{
  public async Task OnAuthorizationAsync(AuthorizationFilterContext context)
  {
    if (!context.HttpContext.Request.Headers.TryGetValue("Authorization", out var token))
    {
      context.Result = new UnauthorizedResult();
      return;
    }

    var configuration = context.HttpContext.RequestServices.GetService<IConfiguration>();

    var claims = Authenticator.VerifyToken(token, configuration.GetValue<string>("SecretKey"));

    if (claims == null)
    {
      context.Result = new UnauthorizedResult();  // status code 401
    }
    else
    {

      // you can access the claims from the context since this is BEFORE the action method
      var requiredClaims = context.ActionDescriptor.EndpointMetadata
                .OfType<RequiredClaimAttribute>()
                .ToList();

                // status code 403
                // checking to see if all the required claims are met.  Using Linc - ALL, e are comparing each of the required claims to the 
                // claims found in request.  We are making sure both the claim Type and the claim Value is equal to each other
                if (requiredClaims != null && !requiredClaims.All(rc => claims.Any(c => c.Type.ToLower() == rc.ClaimType.ToLower() &&
                                                                                          c.Value.ToLower() == rc.ClaimValue.ToLower())))
                //if (requiredClaims != null )
                {
                  context.Result = new StatusCodeResult(403);

                }

    }

  }

}
