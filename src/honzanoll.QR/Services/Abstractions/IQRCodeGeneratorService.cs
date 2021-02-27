using honzanoll.QR.Models;
using System.Drawing;

namespace honzanoll.QR.Services.Abstractions
{
    public interface IQRCodeGeneratorService
    {
        #region Public methods

        Bitmap GeneratePayment(PaymentRequest paymentRequest);

        #endregion
    }
}
