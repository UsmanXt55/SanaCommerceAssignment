using SanaCommerceAssignment.ConfigurableEditor.Portal.Infrastructure.Constants;
using SanaCommerceAssignment.ConfigurableEditor.Portal.Infrastructure.Middlewares;
using SanaCommerceAssignment.ConfigurableEditor.Portal.Infrastructure.Services;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddHttpContextAccessor();
builder.Services.AddSession(options => { options.IdleTimeout = TimeSpan.FromMinutes(30); });
builder.Services.AddControllersWithViews().AddRazorRuntimeCompilation();
builder.Services.AddTransient<IAccountsService, AccountsService>();
builder.Services.AddTransient<ITemplateService, TemplateService>();
builder.Services.AddTransient<IDataService, DataService>();
builder.Services.AddHttpClient(ApiClientConstants.Server, client =>
{
    client.BaseAddress = new Uri(builder.Configuration.GetSection("AppConfiguration:ServerApi").Value!);
});

var app = builder.Build();
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseSession();
app.UseMiddleware<ErrorHandlingMiddleware>();
app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthorization();
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.Run();