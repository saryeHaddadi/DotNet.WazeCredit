using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection.Extensions;
using WazeCredit.Data;
using WazeCredit.Middleware;
using WazeCredit.Service;
using WazeCredit.Service.Interfaces;
using WazeCredit.Service.LifeTimeExamples;
using WazeCredit.Utility.AppSettingsClasses;
using WazeCredit.Utility.DIConfig;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
	options.UseSqlServer(connectionString));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
	.AddEntityFrameworkStores<ApplicationDbContext>();
builder.Services.AddControllersWithViews();

/// <summary>
/// First DI!
/// </summary>
builder.Services.AddTransient<IMarketForcaster, MarketForcaster>();
//builder.Services.AddTransient<IMarketForcaster, MarketForcasterV2>();

/// <summary>
/// IOptions examples
/// </summary>
builder.AddAppSettingsConfig();

/// <summary>
/// LifeTime Examples
/// </summary>
builder.Services.AddSingleton<SingletonService>();
builder.Services.AddScoped<ScopedService>();
builder.Services.AddTransient<TransientService>();

// Other ways of registering services 
//builder.Services.AddSingleton(new MarketForcaster());
//builder.Services.AddSingleton<IMarketForcaster, MarketForcaster>();
//builder.Services.AddSingleton<IMarketForcaster>(new MarketForcaster());
//builder.Services.AddTransient(typeof(IMarketForcaster), typeof(MarketForcaster));

// Making sur that an implementation is not registered two time
// for the same interface
//builder.Services.TryAddTransient<IMarketForcaster, MarketForcaster>();
//builder.Services.TryAddTransient<IMarketForcaster, MarketForcasterV2>();

// Replace, Remove
//builder.Services.Replace(ServiceDescriptor.Transient<IMarketForcaster, MarketForcasterV2>());
//builder.Services.RemoveAll<IMarketForcaster>();

builder.Services.AddScoped<IValidationChecker, AdressValidationChecker>();
builder.Services.AddScoped<IValidationChecker, LoanValidationChecker>();
builder.Services.AddScoped<ILoanValidator, LoanValidator>();
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

app.UseAuthentication();
app.UseAuthorization();
app.UseMiddleware<CustomMiddleware>();

app.MapControllerRoute(
	name: "default",
	pattern: "{controller=Home}/{action=Index}/{id?}");
app.MapRazorPages();

app.Run();
