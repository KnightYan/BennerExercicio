using Benner.Microondas.Comuns;
using Benner.Microondas.Comuns.Extensions;
using Benner.Microondas.Dao.Repositorios;
using Benner.Microondas.Model.Dto;
using System;

namespace Benner.Microondas.Controle.Controles.Validadadores
{
    public class ProgramaValidador
    {
        private static string caractereAquecimenteRepetido = "Caractere de Aquecimento: '{0}' já está sendo utilizado pelo programa: {1} ";
        public void ValidarPrograma(ProgramaDto programa)
        {
            ValidarValoresMetodos(programa);
            ValidarValoresRepetidos(programa);
        }

        private static void ValidarValoresMetodos(ProgramaDto programa)
        {
            if (programa == null)
                throw new ArgumentNullException(string.Format(Constantes.ParametroNullException, "programa"));

            if (programa.Id < 0)
                throw new ArgumentException(string.Format(Constantes.ValorInvalidoException, programa.Id, "Id"));

            if (programa.Nome.IsNullOrWhiteSpace())
                throw new ArgumentException(string.Format(Constantes.ValorInvalidoException, "vazio", "Nome"));

            if (programa.CaractereAquecimento.IsNullOrWhiteSpace())
                throw new ArgumentException(string.Format(Constantes.ValorInvalidoException, "vazio", "Caractere Aquecimento"));

            if (programa.CaractereAquecimento.Length > 1)
                throw new ArgumentException(string.Format(Constantes.ValorInvalidoException, programa.CaractereAquecimento, "Caractere Aquecimento"));

            if (programa.Compatibilidade.IsNullOrWhiteSpace())
                throw new ArgumentException(string.Format(Constantes.ValorInvalidoException, "vazio", "Compatibilidades"));

            if (programa.Tempo.IsNullOrZero())
                throw new ArgumentException(string.Format(Constantes.ValorInvalidoException, "Zero/Vazio", "Tempo"));

            if (programa.Potencia.IsNullOrZero())
                throw new ArgumentException(string.Format(Constantes.ValorInvalidoException, "Zero/Vazio", "Potencia"));

            if (programa.Tempo < 1 || programa.Tempo > 120)
                throw new ArgumentException(string.Format(Constantes.ValorInvalidoException, programa.Tempo, "Tempo"));

            if (programa.Potencia < 1 || programa.Potencia > 10)
                throw new ArgumentException(string.Format(Constantes.ValorInvalidoException, programa.Potencia, "Potencia"));
        }

        private void ValidarValoresRepetidos(ProgramaDto programa)
        {
            var programaIgual = new ProgramaRepositorio().PesquisarProgramaPorCaractere(programa.CaractereAquecimento);
            if (programaIgual != null && programaIgual.Id != programa.Id)
                throw new ArgumentException(string.Format(caractereAquecimenteRepetido, programa.CaractereAquecimento, programaIgual.Nome));
        }
    }
}
