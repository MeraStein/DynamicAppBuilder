using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using DynamicAppBuilder.Client;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri("https://localhost:7770") });


builder.Services.AddScoped<BuildWares>();
builder.Services.AddScoped<Globals>();
builder.Services.AddScoped<Handles>();
builder.Services.AddScoped<ControlMediator>();
builder.Services.AddScoped<PropsService>();


await builder.Build().RunAsync();

