using MQTTnet.Client;
using System.Threading.Tasks;

namespace honzanoll.MQTT.Publishers.Abstractions
{
    public interface IMqttMessagePublisher
    {
        #region Public methods

        Task PublishAsync(string topic, string payload);

        void SetMqttClient(IMqttClient mqttClient);
        
        #endregion
    }
}
