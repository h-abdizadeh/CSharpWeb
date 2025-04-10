using FardaOnlineShop.Data;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages().AddRazorRuntimeCompilation();

builder.Services.AddScoped<DatabaseContext, DatabaseContext>();

const string scheme = "onlineshop";
builder.Services.AddAuthentication(scheme).AddCookie(scheme,option =>
{
    option.AccessDeniedPath = "/Account/login";
    option.LoginPath = "/Account/login";
    option.ExpireTimeSpan = TimeSpan.FromDays(30);//localhost
});

var app = builder.Build();

app.UseStaticFiles();//wwwroot
app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();
//app.MapGet("/", () => "Hello World!");

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllerRoute(
            name: "areas",
            pattern: "{area:exists}/{controller=Groups}/{action=Index}/{id?}"
          );

    endpoints.MapControllerRoute(
        name: "default",
        pattern: "{controller=home}/{action=index}/{id?}");

    
});

app.Run();
