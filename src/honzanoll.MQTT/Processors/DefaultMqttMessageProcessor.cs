using Microsoft.Extensions.Logging;
using honzanoll.MQTT.Processors.Abstractions;
using System.Threading.Tasks;

namespace honzanoll.MQTT.Processors
{
    public  class DefaultMqttMessageProcessor : IMqttMessageProcessor
    {
        #region Fields

        private readonly ILogger<DefaultMqttMessageProcessor> logger;

        #endregion

        #region Constructors

        public DefaultMqttMessageProcessor(ILogger<DefaultMqttMessageProcessor> logger)
        {
            this.logger = logger;
        }

        #endregion

        #region Public methods

        public Task ProcessMessageAsync(string topic, string payload)
        {
            logger.LogInformation($"Received MQTT message from topic {topic}: {payload}");

            return Task.CompletedTask;
        }

        #endregion
    }
}
