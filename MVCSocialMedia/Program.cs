using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MVCSocialMedia.Data;
using MVCSocialMedia.Services;
using Microsoft.AspNetCore.Identity.UI.Services;
using MVCSocialMedia.Models;
using Microsoft.Identity.Client;
using SendGrid.Extensions.DependencyInjection;
using Azure.Identity;
using Microsoft.Azure.AppConfiguration.AspNetCore;
using MVCSocialMedia;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
//var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");

string AppConfigConnectionString = builder.Configuration.GetConnectionString("AppConfig");
builder.Configuration.AddAzureAppConfiguration(AppConfigConnectionString);

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();
builder.Services.AddDefaultIdentity<ApplicationUser>(options =>
{
    options.User.RequireUniqueEmail = true;
    options.SignIn.RequireConfirmedAccount = true;
    options.Tokens.ProviderMap.Add("CustomEmailConfirmation",
        new TokenProviderDescriptor(
            typeof(CustomEmailConfirmationTokenProvider<ApplicationUser>)));
    options.Tokens.EmailConfirmationTokenProvider = "CustomEmailConfirmation";
}).AddRoles<IdentityRole>().AddEntityFrameworkStores<ApplicationDbContext>();


builder.Services.AddControllersWithViews();

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("RequireAdministratorRole",
         policy => policy.RequireRole("Administrator"));
});

builder.Services.AddTransient<CustomEmailConfirmationTokenProvider<ApplicationUser>>();
builder.Services.AddTransient<IEmailSender, EmailSender>();
builder.Services.AddTransient<IUploadedFileChecker, UploadedFileChecker>();
builder.Services.AddTransient<IPostRepository, PostRepository>();
builder.Services.Configure<ConfigSettings>(builder.Configuration.GetSection(""));

builder.Services.ConfigureApplicationCookie(o =>
{
    o.ExpireTimeSpan = TimeSpan.FromDays(5);
    o.SlidingExpiration = true;
});

builder.Services.Configure<DataProtectionTokenProviderOptions>(o =>
    o.TokenLifespan = TimeSpan.FromHours(3));

builder.Services.Configure<ConfigSettings>(builder.Configuration.GetSection("MVCSocialMedia:ConfigSettings"));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.MapRazorPages();

app.Run();
public partial class Program { } 
