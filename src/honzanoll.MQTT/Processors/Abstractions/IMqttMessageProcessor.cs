using System.Threading.Tasks;

namespace honzanoll.MQTT.Processors.Abstractions
{
    public interface IMqttMessageProcessor
    {
        #region Public methods

        Task ProcessMessageAsync(string topic, string payload);

        #endregion
    }
}
