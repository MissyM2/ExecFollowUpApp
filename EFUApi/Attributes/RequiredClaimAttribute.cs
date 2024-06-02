namespace EFUApi;

[AttributeUsage(AttributeTargets.Method | AttributeTargets.Class, AllowMultiple = true)]
public class RequiredClaimAttribute : Attribute
{

  // they do not need a setter because they are initialized in the constructor
  public string ClaimType { get; }
  public string ClaimValue { get; }

  public RequiredClaimAttribute(string claimType, string claimValue)
  {
    this.ClaimType = claimType;
    this.ClaimValue = claimValue;
  }



}
