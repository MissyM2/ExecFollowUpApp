using EFUWebApp;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// this is an endpoint factory
builder.Services.AddHttpClient("EFUApi", client =>
{
    client.BaseAddress = new Uri("http://localhost:5089/api/");
    client.DefaultRequestHeaders.Add("Accept", "application/json");
});

// another endpoint factory
builder.Services.AddHttpClient("AuthorityApi", client =>
{
    client.BaseAddress = new Uri("http://localhost:5089/");
    client.DefaultRequestHeaders.Add("Accept", "application/json");
});

builder.Services.AddSession(options =>
{
    options.Cookie.HttpOnly = true;
    options.IdleTimeout = TimeSpan.FromHours(5);
    options.Cookie.IsEssential = true;
});

builder.Services.AddHttpContextAccessor(); // we need HttpContext in the WeApiExecutor.cs but, because it is a custom class, you cannot access it directly.  So we inject this.
builder.Services.AddControllersWithViews();

builder.Services.AddTransient<IWebApiExecutor, WebApiExecutor>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.UseSession(); // to save token

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
