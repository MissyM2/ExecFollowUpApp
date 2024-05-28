namespace EFUWebApp;

public interface IWebApiExecutor
{
  Task<T?> InvokeGet<T>(string relativeUrl);
}
