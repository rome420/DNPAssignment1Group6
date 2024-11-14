using BlazorApp.Auth;
using BlazorApp.Components;
using BlazorApp.Services;
using Microsoft.AspNetCore.Components.Authorization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container for Razor Components
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

// Register HttpClient to be injected into components and services
builder.Services.AddScoped(sp =>
{
    var handler = new HttpClientHandler();
    handler.ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => true; // Accept any certificate
    return new HttpClient(handler)
    {
        BaseAddress = new Uri("http://localhost:5126")  // Use your actual API URL here
    };
});

// Register application services
builder.Services.AddScoped<IUserService, HttpUserService>();
builder.Services.AddScoped<IPostService, HttpPostService>();
builder.Services.AddScoped<AuthenticationStateProvider, SimpleAuthProvider>();

// CORS configuration (if Blazor app and API are on different ports)
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader();
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline for error handling, HSTS, and static files
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    app.UseHsts();  // Apply HSTS in production
}

// Use HTTPS redirection and serve static files
app.UseHttpsRedirection(); // If you're sure your API is set up for HTTPS
app.UseStaticFiles(); // For serving static files like CSS, JS, images
app.UseAntiforgery();  // For CSRF protection in forms

// Enable CORS (if needed)
app.UseCors("AllowAll");  // This allows cross-origin requests

// Set up routing and components for interactive server-side rendering
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();