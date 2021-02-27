using Microsoft.Extensions.Logging;
using honzanoll.QR.Generators;
using honzanoll.QR.Models;
using honzanoll.QR.Services.Abstractions;
using QRCoder;
using System.Drawing;

namespace honzanoll.QR.Services
{
    public class QRCodeGeneratorService : IQRCodeGeneratorService
    {
        #region Fields

        private readonly PaymentGenerator paymentGenerator;

        private readonly ILogger<QRCodeGeneratorService> logger;

        #endregion

        #region Constructors

        public QRCodeGeneratorService(
            PaymentGenerator paymentGenerator,
            ILogger<QRCodeGeneratorService> logger)
        {
            this.paymentGenerator = paymentGenerator;

            this.logger = logger;
        }

        #endregion

        #region Public methods

        public Bitmap GeneratePayment(PaymentRequest paymentRequest)
        {
            QRCodeGenerator qrGenerator = new QRCodeGenerator();

            string data = paymentGenerator.GetPaymentData(paymentRequest);
            QRCodeData qrCodeData = qrGenerator.CreateQrCode(data, QRCodeGenerator.ECCLevel.Q);

            logger.LogInformation("Generated payment QR code: " + data);

            return new QRCode(qrCodeData).GetGraphic(20);
        }

        #endregion
    }
}
