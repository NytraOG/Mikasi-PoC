using Domain.Data.Entities.Security;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Blackshield.Auth;

// Minimal-API-Endpoints, die im echten HTTP-Kontext laufen (nicht im interaktiven
// Circuit) – nur hier lässt sich der Auth-Cookie in den Response-Header schreiben.
// Die /auth/login-Seite postet ihr Formular hierher.
public static class AuthEndpoints
{
    public static IEndpointRouteBuilder MapAuthEndpoints(this IEndpointRouteBuilder endpoints)
    {
        var group = endpoints.MapGroup("/auth");

        group.MapPost("/login", LoginAsync);
        group.MapPost("/logout", LogoutAsync).RequireAuthorization();

        return endpoints;
    }

    private static async Task<IResult> LoginAsync(
        SignInManager<ApplicationUser> signInManager,
        UserManager<ApplicationUser>   userManager,
        [FromForm] string              identifier,   // E-Mail ODER Accountname
        [FromForm] string              password,
        [FromForm] bool                rememberMe = false,
        [FromForm] string?             returnUrl  = null)
    {
        // Login per E-Mail oder Benutzername zulassen.
        var user = await userManager.FindByEmailAsync(identifier)
                ?? await userManager.FindByNameAsync(identifier);

        if (user is not null)
        {
            var result = await signInManager.PasswordSignInAsync(
                user, password, isPersistent: rememberMe, lockoutOnFailure: true);

            if (result.Succeeded)
                return Results.LocalRedirect(NormalizeReturnUrl(returnUrl));

            if (result.IsLockedOut)
                return RedirectToLogin("lockout", returnUrl);

            if (result.IsNotAllowed)
                return RedirectToLogin("notallowed", returnUrl);
        }

        // Bewusst keine Auskunft, ob User oder Passwort falsch war.
        return RedirectToLogin("invalid", returnUrl);
    }

    private static async Task<IResult> LogoutAsync(SignInManager<ApplicationUser> signInManager)
    {
        await signInManager.SignOutAsync();
        return Results.LocalRedirect("/auth/login");
    }

    private static IResult RedirectToLogin(string error, string? returnUrl)
        => Results.LocalRedirect(
            $"/auth/login?error={error}&returnUrl={Uri.EscapeDataString(returnUrl ?? "/")}");

    // Open-Redirect-Schutz: nur lokale Pfade zulassen.
    private static string NormalizeReturnUrl(string? returnUrl)
        => string.IsNullOrWhiteSpace(returnUrl) || !returnUrl.StartsWith('/')
            ? "/"
            : returnUrl;
}
