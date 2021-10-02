using AutoMapper;
using Microsoft.Extensions.DependencyInjection;

namespace Benner.Microondas.Mapeamento
{
    public class ConfiguracaoMapeamento
    {
        public static void Configurar(IServiceCollection serviceCollection)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new DtoParaEntidade());
                cfg.AddProfile(new EntidadeParaDto());
            });

            IMapper mapper = config.CreateMapper();
            serviceCollection.AddSingleton(mapper);
        }
    }
}
