using honzanoll.QR.Consts.Enums;
using honzanoll.QR.Models;
using System.Globalization;

namespace honzanoll.QR.Generators
{
    public class PaymentGenerator
    {
        #region Public methods

        public string GetPaymentData(PaymentRequest request)
        {
            string data = $"SPD*1.0*";
            data += $"ACC:{request.IBAN}*";
            data += $"AM:{request.Amount.ToString(new NumberFormatInfo { NumberDecimalSeparator = "." })}*";
            data += $"CC:{request.Currency.GetISO()}*";

            if (request.DueDate.HasValue)
                data += $"DT:{request.DueDate.Value:YYYYMMDD}*";

            if (request.ConstantSymbol > 0)
                data += $"X-KS:{request.ConstantSymbol:##########}*";

            if (request.SpecificSymbol > 0)
                data += $"X-SS:{request.SpecificSymbol:##########}*";

            if (request.VariableSymbol > 0)
                data += $"X-VS:{request.VariableSymbol:##########}*";

            data += $"MSG:{request.Message}";

            return data;
        }

        #endregion
    }
}
