using IncomeCalculator.Shared.Interfaces;
using IncomeCalculator.WASM;
using IncomeCalculator.WASM.Services;
using IncomeCalculator.WASM.ViewModel;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.Configuration.GetValue<string>("BaseAPIUrl")) });

builder.Services.AddAuthorizationCore();

builder.Services.AddScoped<IMinWageService, MinWageApiService>();
builder.Services.AddScoped<IMessageService, MessageService>();
builder.Services.AddScoped<FinancialDetails>();
builder.Services.AddScoped<WorkDetails>();
builder.Services.AddScoped<TCApiService>();
builder.Services.AddScoped<TCDataService>();
await builder.Build().RunAsync();
