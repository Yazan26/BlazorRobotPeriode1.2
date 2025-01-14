using SimpleMqtt;
using Microsoft.Extensions.Configuration;

public class MqttService : IMqttService, IDisposable
{
    private SimpleMqttClient _mqttClient;
    private readonly IConfiguration _configuration;

    public event EventHandler<SimpleMqttMessage>? OnMessageReceived;

    public MqttService(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public void InitializeClient(string clientId)
    {
        _mqttClient = SimpleMqttClient.CreateSimpleMqttClientForHiveMQ(clientId, _configuration);
        _mqttClient.OnMessageReceived += (sender, message) => OnMessageReceived?.Invoke(sender, message);
    }

    public async Task PublishMessage(string message, string topic, bool retain = false)
    {
        await _mqttClient.PublishMessage(message, topic, retain);
    }

    public async Task SubscribeToTopic(string topic)
    {
        await _mqttClient.SubscribeToTopic(topic);
    }

    public void Dispose()
    {
        _mqttClient.Dispose();
    }
}