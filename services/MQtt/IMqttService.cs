using SimpleMqtt;

public interface IMqttService
{
    event EventHandler<SimpleMqttMessage>? OnMessageReceived;
    Task PublishMessage(string message, string topic, bool retain = false);
    Task SubscribeToTopic(string topic);
    void InitializeClient(string clientId);
    void Dispose();
}