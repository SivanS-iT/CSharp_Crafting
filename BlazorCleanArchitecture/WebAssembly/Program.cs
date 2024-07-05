using Application.Queries.EmployeeQuery;
using Application.Services;
using Blazored.Toast;
using Infrastructure.Handlers.EmployeeHandler;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using WebAssembly;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri("https://localhost:7101/") });
builder.Services.AddScoped<IEmployeeService, EmployeeService>();
builder.Services.AddBlazoredToast();
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(GetEmployeeListHandler).Assembly));


await builder.Build().RunAsync();
