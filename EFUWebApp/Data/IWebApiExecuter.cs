namespace EFUWebApp;

public interface IWebApiExecuter
{
  Task<T?> InvokeGet<T>(string relativeUrl);
}
