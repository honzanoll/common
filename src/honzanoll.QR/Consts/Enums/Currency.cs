using System;

namespace honzanoll.QR.Consts.Enums
{
    public enum Currency
    {
        CzechCrown,

        Euro
    }

    public static class CurrencyExtension
    {
        public static string GetISO(this Currency currency)
        {
            switch (currency)
            {
                case Currency.CzechCrown: return "CZK";
                case Currency.Euro: return "EUR";
                default: throw new ArgumentOutOfRangeException(nameof(currency));
            }
        }
    }
}
