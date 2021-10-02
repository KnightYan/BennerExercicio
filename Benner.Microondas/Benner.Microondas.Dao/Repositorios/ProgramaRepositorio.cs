using Benner.Microondas.Model.Entidades;
using System.Collections.Generic;
using System.Linq;

namespace Benner.Microondas.Dao.Repositorios
{
    public class ProgramaRepositorio
    {
        public void SalvarPrograma(ProgramaEntidade programa)
        {
            if (programa == null)
                return;

            if (programa.Id == 0)
                Banco.BancoDados.Insert(programa);
            else
                Banco.BancoDados.Update(programa);
        }

        public ProgramaEntidade ObtemPrograma(int id)
        {
            var teste = Banco.BancoDados.Table<ProgramaEntidade>();

            return Banco.BancoDados.Table<ProgramaEntidade>().FirstOrDefault(x => x.Id == id);
        }

        public List<ProgramaEntidade> ObtemProgramas()
        {
            return Banco.BancoDados.Table<ProgramaEntidade>().ToList();
        }

        public bool PossuiProgramasCadastrados()
        {
            return Banco.BancoDados.Table<ProgramaEntidade>().Any();
        }

        public ProgramaEntidade PesquisarProgramaPorCaractere(string caractere)
        {
            return Banco.BancoDados.Table<ProgramaEntidade>().FirstOrDefault(x => x.CaractereAquecimento == caractere);
        }

        public List<ProgramaEntidade> PesquisarProgramaPorAlimentoCompativel(string alimento)
        {
            return Banco.BancoDados.Table<ProgramaEntidade>()
                .Where(x => x.Compatibilidade.Contains(alimento)).ToList();
        }
    }
}
