using Microsoft.Extensions.DependencyInjection;
using honzanoll.QR.Generators;
using honzanoll.QR.Services;
using honzanoll.QR.Services.Abstractions;

namespace honzanoll.QR.Extensions
{
    public static class IServiceCollectionExtensions
    {
        public static void AddQRGenerator(this IServiceCollection services)
        {
            services.AddScoped<PaymentGenerator>();

            services.AddScoped<IQRCodeGeneratorService, QRCodeGeneratorService>();
        }
    }
}
