using MQTTnet.Client;
using honzanoll.MQTT.Publishers.Abstractions;
using System;
using System.Threading.Tasks;

namespace honzanoll.MQTT.Publishers
{
    public class DefaultMqttMessagePublisher : IMqttMessagePublisher
    {
        #region Protectec members

        protected IMqttClient mqttClient;

        #endregion

        #region Public methods

        public async Task PublishAsync(string topic, string payload)
        {
            if (mqttClient == null)
                throw new NullReferenceException("MQTT client is not set");

            await mqttClient.PublishAsync(topic, payload);
        }

        public virtual void SetMqttClient(IMqttClient mqttClient)
        {
            if (this.mqttClient != null)
                throw new InvalidOperationException("MQTT client is set yet");

            this.mqttClient = mqttClient;
        }

        #endregion
    }
}
