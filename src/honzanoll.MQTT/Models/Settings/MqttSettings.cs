using System.Collections.Generic;

namespace honzanoll.MQTT.Models.Settings
{
    public class MqttSettings
    {
        #region Properties

        public string ClientId { get; set; }

        public string Host { get; set; }

        public int Port { get; set; }

        public string Username { get; set; }

        public string Password { get; set; }

        public IEnumerable<string> SubscribedTopics { get; set; }

        #endregion
    }
}
