using Blackshield.Components;
using Blackshield.Components.Account;
using Domain.Data.Contexts;
using Domain.Data.Entities.Security;
using Domain.Data.Interceptors;
using Domain.Services;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MudBlazor.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
       .AddInteractiveServerComponents();

builder.Services.AddMudServices();

builder.Services.AddCascadingAuthenticationState();
builder.Services.AddScoped<IdentityRedirectManager>();
builder.Services.AddScoped<AuthenticationStateProvider, IdentityRevalidatingAuthenticationStateProvider>();

builder.Services.AddAuthentication(options =>
        {
            options.DefaultScheme       = IdentityConstants.ApplicationScheme;
            options.DefaultSignInScheme = IdentityConstants.ExternalScheme;
        })
       .AddIdentityCookies();

builder.Services.ConfigureApplicationCookie(options =>
{
    options.LoginPath        = "/auth/login";
    options.AccessDeniedPath = "/auth/login";
});

builder.Services.AddHttpContextAccessor();
builder.Services.AddSingleton<AuditInterceptor>();

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");

builder.Services.AddPooledDbContextFactory<DefaultContext>((sp, opt) =>
{
    opt.UseNpgsql(connectionString, o => o.SetPostgresVersion(18, 0))
       .UseSnakeCaseNamingConvention()
       .AddInterceptors(sp.GetRequiredService<AuditInterceptor>());
});

builder.Services.AddScoped<DefaultContext>(sp => sp.GetRequiredService<IDbContextFactory<DefaultContext>>().CreateDbContext());
builder.Services.AddTransient<WohnobjektService>();



builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddIdentityCore<ApplicationUser>(options =>
        {
            options.SignIn.RequireConfirmedAccount = true;
            options.Stores.SchemaVersion           = IdentitySchemaVersions.Version3;
        })
       .AddEntityFrameworkStores<DefaultContext>()
       .AddSignInManager()
       .AddDefaultTokenProviders();

builder.Services.AddSingleton<IEmailSender<ApplicationUser>, IdentityNoOpEmailSender>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
    app.UseMigrationsEndPoint();
else
{
    app.UseExceptionHandler("/Error", true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseStatusCodePagesWithReExecute("/not-found", createScopeForStatusCodePages: true);
app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.UseAntiforgery();

app.MapStaticAssets();

app.MapRazorComponents<App>()
   .AddInteractiveServerRenderMode();

// Add additional endpoints required by the Identity /Account Razor components.
app.MapAdditionalIdentityEndpoints();

app.Run();