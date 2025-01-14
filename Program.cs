using DependencyInjectionExample.Data;
using WebsiteRobotPeriode1._2.Components;
using SimpleMqtt;
using Microsoft.EntityFrameworkCore;


var builder = WebApplication.CreateBuilder(args);




// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddScoped<IDatabaseService, DatabaseService>();
builder.Services.AddScoped<IMqttService, MqttService>();
var config = builder.Configuration;
var sqlConnectionString = config.GetConnectionString("DefaultConnection");  
    builder.Services.AddSingleton<IUserRepository, SqlUserRepostitory>(o => new SqlUserRepostitory(sqlConnectionString));
builder.Services.AddSingleton<WeatherService>();
builder.Services.AddSingleton<SimpleMqttClient>(provider =>
{
    var configuration = provider.GetRequiredService<IConfiguration>();
    var client = SimpleMqttClient.CreateSimpleMqttClientForHiveMQ("2aa2fd");
    return client;
});
builder.Services.AddHostedService<MqttMessageProcessingService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

var configuration = app.Services.GetRequiredService<IConfiguration>();

app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();  