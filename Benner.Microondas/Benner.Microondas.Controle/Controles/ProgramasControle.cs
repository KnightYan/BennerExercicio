using AutoMapper;
using Benner.Microondas.Comuns.Configuracoes;
using Benner.Microondas.Controle.Controles.Validadadores;
using Benner.Microondas.Dao.Repositorios;
using Benner.Microondas.Model.Dto;
using Benner.Microondas.Model.Entidades;
using System.Collections.Generic;

namespace Benner.Microondas.Controle.Controles
{
    public class ProgramasControle
    {
        public void IniciarProgramas()
        {
            var repositorio = new ProgramaRepositorio();

            if(!repositorio.PossuiProgramasCadastrados())
            {
                var programas = new ProgramaGerador().GerarProgramasPadroes();
                var mapeador = (IMapper)ServiceProviderIntance.ServiceProvider.GetService(typeof(IMapper));

                foreach(var programa in programas)
                {
                    var entidade = mapeador.Map<ProgramaEntidade>(programa);

                    new ProgramaRepositorio().SalvarPrograma(entidade);
                }
            }
        }

        public void SalvarPrograma(ProgramaDto programa)
        {
            new ProgramaValidador().ValidarPrograma(programa);

            var mapeador = (IMapper)ServiceProviderIntance.ServiceProvider.GetService(typeof(IMapper));
            var entidade = mapeador.Map<ProgramaEntidade>(programa);

            new ProgramaRepositorio().SalvarPrograma(entidade);
        }

        public List<ProgramaDto> ObterProgramas()
        {
            var listaEntidades = new ProgramaRepositorio().ObtemProgramas();

            var mapeador = (IMapper)ServiceProviderIntance.ServiceProvider.GetService(typeof(IMapper));
            return mapeador.Map<List<ProgramaDto>>(listaEntidades);
        }
    }
}
