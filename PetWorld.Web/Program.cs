using Microsoft.EntityFrameworkCore;
using PetWorld.Application.Configuration;
using PetWorld.Application.Services;
using PetWorld.Application.Services.Agents;
using PetWorld.Infrastructure.Data;
using PetWorld.Infrastructure.Repositories;

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

// Register Agent Services
builder.Services.AddSingleton<WriterAgent>();
builder.Services.AddSingleton<CriticAgent>();
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<IChatService, ChatService>();

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