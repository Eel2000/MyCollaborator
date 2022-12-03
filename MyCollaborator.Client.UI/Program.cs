using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using MyCollaborator.Client.UI;
using MyCollaborator.Client.UI.Interops;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

#if DEBUG
builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri("https://localhost:7023") });
#elif RELEASE
builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(/*specify the prod api base url*/) });
#endif

builder.Services.AddScoped<LocalStorageInterop>();

await builder.Build().RunAsync();