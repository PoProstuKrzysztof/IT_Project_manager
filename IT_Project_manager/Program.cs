using IT_Project_manager.Controllers;
using IT_Project_manager.Models;
using IT_Project_manager.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using IT_Project_manager.Data;
using IT_Project_manager.Areas.Identity.Data;

var builder = WebApplication.CreateBuilder( args );
var connectionString = builder.Configuration.GetConnectionString( "Default" ) ?? throw new InvalidOperationException( "Connection string 'Default' not found." );

// Add services to the container.

builder.Services.AddDbContext<AppDbContext>( options =>
    options.UseSqlServer( connectionString ) );
builder.Services.AddDefaultIdentity<ApplicationUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddEntityFrameworkStores<AppDbContext>();
builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();
builder.Services.AddScoped<IMemberService, MembersServiceEF>();
builder.Services.AddScoped<IManagerService, ManagersServiceEF>();
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

app.UseAuthentication();;
app.UseAuthorization();


app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}" );
app.MapRazorPages();

app.Run();