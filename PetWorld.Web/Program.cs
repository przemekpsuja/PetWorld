using Microsoft.EntityFrameworkCore;
using PetWorld.Application.Configuration;
using PetWorld.Infrastructure.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

// Add DbContext
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<PetWorldDbContext>(options =>
    options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)));

// Configure Agent Framework
var agentConfig = builder.Configuration.GetSection("AgentSettings").Get<AgentConfiguration>();
if (agentConfig != null)
{
    builder.Services.AddSingleton(agentConfig);
}

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<PetWorld.Web.App>()
    .AddInteractiveServerRenderMode();

app.Run();