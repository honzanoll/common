using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MQTTnet;
using MQTTnet.Client;
using MQTTnet.Client.Connecting;
using MQTTnet.Client.Disconnecting;
using MQTTnet.Client.Options;
using honzanoll.Common.Workers.Services;
using honzanoll.MQTT.Models.Settings;
using honzanoll.MQTT.Processors.Abstractions;
using honzanoll.MQTT.Publishers.Abstractions;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace honzanoll.MQTT.Services
{
    public class MqttClientHostedService<
        TMqttMessageProcessor,
        TMqttMessagePublisher,
        TMqttSettings> : HostedService
        where TMqttMessageProcessor : IMqttMessageProcessor
        where TMqttMessagePublisher : IMqttMessagePublisher
        where TMqttSettings : MqttSettings, new()
    {
        #region Fields

        private readonly IMqttClient mqttClient;

        private readonly TMqttMessageProcessor mqttMessageProcessor;

        private readonly TMqttSettings mqttSettings;

        private readonly ILogger<MqttClientHostedService<TMqttMessageProcessor, TMqttMessagePublisher, TMqttSettings>> logger;

        #endregion

        #region Protected members

        protected override string ServiceName => "honzanoll.MqttClient.Service";

        #endregion

        #region Constructors

        public MqttClientHostedService(
            TMqttMessageProcessor mqttMessageProcessor,
            TMqttMessagePublisher mqttMessagePublisher,
            IOptions<TMqttSettings> mqttSettings,
            ILogger<MqttClientHostedService<TMqttMessageProcessor, TMqttMessagePublisher, TMqttSettings>> logger) : base(logger)
        {
            this.mqttMessageProcessor = mqttMessageProcessor;

            this.mqttSettings = mqttSettings.Value;

            this.logger = logger;

            mqttClient = BuildMqttClient();

            mqttMessagePublisher.SetMqttClient(mqttClient);
        }

        #endregion

        #region Protected methods

        protected virtual async Task ConnectAsync(CancellationToken cancellationToken)
        {
            MqttClientOptionsBuilder mqttClientOptionsBuilder = new MqttClientOptionsBuilder()
                .WithTcpServer(mqttSettings.Host, mqttSettings.Port)
                .WithClientId(mqttSettings.ClientId);

            if (!string.IsNullOrEmpty(mqttSettings.Username))
                mqttClientOptionsBuilder.WithCredentials(mqttSettings.Username, mqttSettings.Password);

            await mqttClient.ConnectAsync(mqttClientOptionsBuilder.Build(), cancellationToken);
        }

        protected virtual async Task SubscribeAsync()
        {
            if (mqttSettings.SubscribedTopics != null)
            {
                MqttTopicFilter[] topics = mqttSettings.SubscribedTopics.Select(t => new MqttTopicFilterBuilder().WithTopic(t).Build()).ToArray();

                await mqttClient.SubscribeAsync(topics);
            }
        }

        protected override async Task OnStartingAsync(CancellationToken cancellationToken)
        {
            await ConnectAsync(cancellationToken);

            await base.OnStartingAsync(cancellationToken);
        }

        protected override async Task OnStoppingAsync(CancellationToken cancellationToken)
        {
            mqttClient.UseDisconnectedHandler((Action<MqttClientDisconnectedEventArgs>)null);
            await mqttClient.DisconnectAsync(null, cancellationToken);

            mqttClient.UseApplicationMessageReceivedHandler((Action<MqttApplicationMessageReceivedEventArgs>)null);
            mqttClient.UseConnectedHandler((Action<MqttClientConnectedEventArgs>)null);
            mqttClient.UseDisconnectedHandler((Action<MqttClientDisconnectedEventArgs>)null);
            mqttClient.Dispose();

            await base.OnStoppingAsync(cancellationToken);
        }

        protected virtual async Task OnConnectedAsync(MqttClientConnectedEventArgs arg)
        {
            try
            {
                await SubscribeAsync();
            }
            catch (Exception e)
            {
                logger.LogError(e, $"{ServiceName}: Subscribing MQTT topics error");
            }
        }

        protected virtual async Task OnDisconnectedAsync(MqttClientDisconnectedEventArgs arg)
        {
            logger.LogWarning("Disconnected from MQTT broker");

            await Task.Delay(TimeSpan.FromSeconds(5));

            try
            {
                await ConnectAsync(CancellationToken.None);
            }
            catch
            {
                logger.LogWarning("Reconnecting failed");

                await Task.Delay(TimeSpan.FromSeconds(30));

                await ConnectAsync(CancellationToken.None);
            }
        }

        protected virtual async Task OnMessageReceivedAsync(MqttApplicationMessageReceivedEventArgs args)
        {
            string payload = args.ApplicationMessage.ConvertPayloadToString();

            logger.LogInformation("Received from topic: {0}, payload: {1}", args.ApplicationMessage.Topic, payload);

            try
            {
                await mqttMessageProcessor.ProcessMessageAsync(args.ApplicationMessage.Topic, payload);
            }
            catch (Exception e)
            {
                logger.LogError(e, $"{ServiceName}-{mqttMessageProcessor.GetType().Name}: Received message processing error");
            }
        }

        #endregion

        #region Private methods

        private IMqttClient BuildMqttClient()
        {
            IMqttFactory factory = new MqttFactory();
            IMqttClient mqttClient = factory.CreateMqttClient();

            mqttClient.UseConnectedHandler(OnConnectedAsync);
            mqttClient.UseDisconnectedHandler(OnDisconnectedAsync);
            mqttClient.UseApplicationMessageReceivedHandler(OnMessageReceivedAsync);

            return mqttClient;
        }

        #endregion
    }
}
