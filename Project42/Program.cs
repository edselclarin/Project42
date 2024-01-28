using Microsoft.EntityFrameworkCore;
using Project42.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddScoped<ClipboardService>();
builder.Services.AddScoped<PasswordGeneratorService>();
builder.Services.AddScoped<ChoreBoardService>();
builder.Services.AddDbContext<ChoresContext>(options => 
    options.UseSqlServer(builder.GetDefaultConnectionString()));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
else
{
    app.UseExceptionHandler("/Error");
    app.UseStatusCodePagesWithReExecute("/Error/{0}");
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();

public static class WebApplicationBuilderExtension
{
    public static string GetDefaultConnectionString(this WebApplicationBuilder builder)
    {
        string connStr = builder.Configuration.GetConnectionString("DefaultConnection");
        return connStr.Replace("pwd=;", $"pwd={password_};");
    }

    // TODO: needs to be secured
    private static string password_ = "AwareCraneAward-4872";
}
