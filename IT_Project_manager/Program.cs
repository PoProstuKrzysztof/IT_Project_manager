using IT_Project_manager.Controllers;
using IT_Project_manager.Models;
using IT_Project_manager.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using IT_Project_manager.Data;
using IT_Project_manager.Areas.Identity.Data;
using IT_Project_manager.Areas.Identity;

var builder = WebApplication.CreateBuilder( args );
var connectionString = builder.Configuration.GetConnectionString( "Default" ) ?? throw new InvalidOperationException( "Connection string 'Default' not found." );

// Add services to the container.

builder.Services.AddDbContext<AppDbContext>( options =>
options.UseSqlServer( connectionString ) );
builder.Services.AddDefaultIdentity<ApplicationUser>( options => options.SignIn.RequireConfirmedAccount = true )
    .AddEntityFrameworkStores<AppDbContext>();
builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();
builder.Services.AddScoped<IMemberService, MembersServiceEF>();
builder.Services.AddScoped<IManagerService, ManagersServiceEF>();



builder.Services.Configure<IdentityOptions>( options =>
{
    //Password settings
    //options.Password.RequireDigit = true;
    //options.Password.RequireNonAlphanumeric = true;
    //options.Password.RequiredLength = 6;
    //options.Password.RequiredUniqueChars = 1;

    // Lockout settings
    options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes( 1 );
    options.Lockout.MaxFailedAccessAttempts = 5;
    options.Lockout.AllowedForNewUsers = true;

    //User settings 
    options.User.AllowedUserNameCharacters =
    "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";
    options.User.RequireUniqueEmail = false;
    
});

builder.Services.ConfigureApplicationCookie( options =>
{
    //Cookie settings
    options.Cookie.HttpOnly = true;
    options.ExpireTimeSpan = TimeSpan.FromMinutes( 5 );

    options.LoginPath = "/Account/Login";
    options.SlidingExpiration = true;

} );

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler( "/Home/Error" );
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication(); ;
app.UseAuthorization();



app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}" );
app.MapRazorPages();

app.Run();