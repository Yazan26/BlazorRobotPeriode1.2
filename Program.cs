using DependencyInjectionExample.Data;
using WebsiteRobotPeriode1._2.Components;
using DependencyInjectionExample.Data;
using SimpleMqtt;

var builder = WebApplication.CreateBuilder(args);




// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();
    var sqlConnectionString = "";  //to do : replace with user secrets
    builder.Services.AddSingleton<IUserRepository, SqlUserRepostitory>(o => new SqlUserRepostitory(sqlConnectionString));
builder.Services.AddSingleton<WeatherService>();
builder.Services.AddSingleton<SimpleMqttClient>(provider =>
{
    var configuration = provider.GetRequiredService<IConfiguration>();
    var client = SimpleMqttClient.CreateSimpleMqttClientForHiveMQ("2aa2fd8d", configuration);
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
var simpleMqttClient = new SimpleMqttClient(new()
{
    Host = "", 
    Port = 8883,
    ClientId = "webapp",
    TimeoutInMs = 5_000, 
    UserName = configuration["MqttClient:UserName"], // staat in user-secrets
    Password = "MqttCLient:Password" // staat in user-secrets
});


app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();  