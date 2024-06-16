using SheypoorChi.Core.Interface;
using SheypoorChi.Core.Service;
using SheypoorChi.DataLayer.Context;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages().AddRazorRuntimeCompilation();

builder.Services.AddScoped<DatabaseContext, DatabaseContext>();
builder.Services.AddScoped<IAdmin, AdminService>();

const string scheme = "sheypoorchi";
builder.Services.AddAuthentication().AddCookie(scheme, option =>
{
    option.LoginPath = "/account/login";
    option.AccessDeniedPath = "/account/login";
    option.ExpireTimeSpan = TimeSpan.FromDays(30);
});

var app = builder.Build();

app.UseRouting();
app.UseStaticFiles();

app.UseAuthentication();
app.UseAuthorization();

//app.MapGet("/", () => "Hello World!");

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllerRoute(
        name: "area",
        pattern: "{area:exists}/{controller=panel}/{action=index}/{id?}");

    endpoints.MapControllerRoute(
        name: "default",
        pattern: "{controller=home}/{action=index}/{id?}");


});

app.Run();
