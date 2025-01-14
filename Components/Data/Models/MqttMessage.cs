namespace DependencyInjectionExample.Data
{
    public class MqttMessage
    {
        public int Id { get; set; }
        public string Topic { get; set; }
        public string Message { get; set; }
        public DateTime Timestamp { get; set; }
    }
}