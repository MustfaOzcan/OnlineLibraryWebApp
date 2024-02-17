using Microsoft.EntityFrameworkCore;
using WebUygulamaProje1.Models;
using WebUygulamaProje1.Utility;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using System.Globalization;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Host;
using System.Reflection;
using Microsoft.AspNetCore.Localization;
using Microsoft.CodeAnalysis.Options;
using Microsoft.Extensions.Options;

var builder = WebApplication.CreateBuilder(args);


// Add services to the container.
builder.Services.AddControllersWithViews();
//UygulamaDbContexKoPRu
builder.Services.AddDbContext<UygulamaDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddIdentity<IdentityUser,IdentityRole>(options => options.SignIn.RequireConfirmedAccount = true).AddEntityFrameworkStores<UygulamaDbContext>().AddDefaultTokenProviders();
//RAZOR sayfalarını kullanmak icin login/register
builder.Services.AddRazorPages();

//_kitapTuruRepository => DEpendency Injection
builder.Services.AddScoped<IKitapTuruRepository, KitapTuruRepository>();
builder.Services.AddScoped<IKitapRepository, KitapRepository>();
builder.Services.AddScoped<IKiralamaRepository, KiralamaRepository>();
builder.Services.AddScoped<IEmailSender, EmailSender>();

var app = builder.Build();



// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

//app.UseRequestLocalization();
app.UseHttpsRedirection();
app.UseStaticFiles();

//eklendidil
//app.UseRequestLocalization(app.Services.GetRequiredService<IOptions<RequestLocalizationOptions>>().Value);

app.UseRouting();

app.UseAuthorization();
//Razor pageler artık calısır login/register
app.MapRazorPages();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=HomePage}/{id?}");

app.Run();
