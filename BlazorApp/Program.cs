using BlazorApp.Auth;
using BlazorApp.Components;
using BlazorApp.Services;
using Microsoft.AspNetCore.Components.Authorization;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();


builder.Services.AddScoped(sp =>
{
    var handler = new HttpClientHandler();
    handler.ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => true; // Accept any certificate
    return new HttpClient(handler)
    {
        BaseAddress = new Uri("http://localhost:5126")  
    };
});

// Register application services
builder.Services.AddScoped<IUserService, HttpUserService>();
builder.Services.AddScoped<IPostService, HttpPostService>();
builder.Services.AddScoped<AuthenticationStateProvider, SimpleAuthProvider>();


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


if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    app.UseHsts();  // Apply HSTS in production
}


app.UseHttpsRedirection(); // If you're sure your API is set up for HTTPS
app.UseStaticFiles(); // For serving static files like CSS, JS, images
app.UseAntiforgery();  // For CSRF protection in forms


app.UseCors("AllowAll");  // This allows cross-origin requests


app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();