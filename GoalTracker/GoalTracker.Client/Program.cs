using GoalTracker.Client.Feautures.LifeAreas;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using MudBlazor.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);

builder.Services.AddAuthorizationCore();
builder.Services.AddCascadingAuthenticationState();
builder.Services.AddAuthenticationStateDeserialization();

// HttpClient для вызова Server API
builder.Services.AddHttpClient("GoalTrackerAPI", client =>
    client.BaseAddress = new Uri(builder.HostEnvironment.BaseAddress));
builder.Services.AddMudServices();
builder.Services.AddScoped(sp =>
    sp.GetRequiredService<IHttpClientFactory>().CreateClient("GoalTrackerAPI"));
builder.Services.AddScoped<GoalTracker.Client.Feautures.Goals.GoalsApiClient>();
builder.Services.AddScoped<GoalTracker.Client.Feautures.LifeAreas.LifeAreaApiClient>();

await builder.Build().RunAsync();