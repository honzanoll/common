using honzanoll.QR.Consts.Enums;
using System;

namespace honzanoll.QR.Models
{
    public class PaymentRequest
    {
        #region Properties

        public string IBAN { get; set; }

        public decimal Amount { get; set; }

        public Currency Currency { get; set; } = Currency.CzechCrown;

        public DateTime? DueDate { get; set; }

        public string Message { get; set; }

        public int ConstantSymbol { get; set; }

        public int SpecificSymbol { get; set; }

        public int VariableSymbol { get; set; }

        #endregion
    }
}
