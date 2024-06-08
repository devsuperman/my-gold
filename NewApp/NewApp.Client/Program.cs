using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using NewApp.Client.Services;
using BitzArt.Blazor.Auth;
using Dominio.Interfaces;

var builder = WebAssemblyHostBuilder.CreateDefault(args);

builder.Services.AddScoped<ICategoriasRepository, CategoriasService>();
builder.Services.AddScoped<IGastosRepository, GastosService>();

builder.Services.AddScoped(http => new HttpClient
{
    BaseAddress = new Uri(builder.HostEnvironment.BaseAddress)
});

builder.AddBlazorAuth();

await builder.Build().RunAsync();
