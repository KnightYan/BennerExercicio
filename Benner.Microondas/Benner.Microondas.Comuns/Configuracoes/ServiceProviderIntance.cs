using Benner.Microondas.Mapeamento;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace Benner.Microondas.Comuns.Configuracoes
{
    public class ServiceProviderIntance
    {
        private static IServiceProvider serviceProvider;

        public static IServiceProvider ServiceProvider
        {
            get
            {
                if (serviceProvider == null)
                {
                    var serviceCollection = new ServiceCollection();
                    ConfigurarServicos(serviceCollection);

                    serviceProvider = serviceCollection.BuildServiceProvider();
                }

                return serviceProvider;
            }
        }

        private static void ConfigurarServicos(IServiceCollection services)
        {
            ConfiguracaoMapeamento.Configurar(services);
        }
    }
}
