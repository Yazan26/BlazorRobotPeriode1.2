using SimpleMqtt;

public class MqttMessageProcessingService : IHostedService
{
    private readonly IUserRepository _userRepository;
    private readonly SimpleMqttClient _mqttClient;

    public MqttMessageProcessingService(IUserRepository userRepository, SimpleMqttClient mqttClient)
    {
      	_userRepository = userRepository;  
      	_mqttClient = mqttClient;
        
        _mqttClient.OnMessageReceived += (sender, args) => {
            Console.WriteLine($"Incoming MQTT message on {args.Topic}:{args.Message}");
            // sla hier de user op
        };
    }
    
    // Constructor for the MqttMessageProcessingService class
    public MqttMessageProcessingService()
    {
        // Initialization code can go here
    }

    // This method is called when the host starts
    public async Task StartAsync(CancellationToken cancellationToken)
    {
      await _mqttClient.SubscribeToTopic("test");
    }

    // This method is called when the host stops
    public async Task StopAsync(CancellationToken cancellationToken)
    {
       _mqttClient.Dispose();
    }
}