using Benner.Microondas.Comuns;
using Benner.Microondas.Model.Entidades;
using SQLite;

namespace Benner.Microondas.Dao
{
    public class Banco 
    {
        private static SQLiteConnection bancoDados;

        public static SQLiteConnection BancoDados
        {
            get
            {
                if (bancoDados == null)
                {
                    bancoDados = new SQLiteConnection(Constantes.CaminhoBanco, Constantes.Flags);
                    bancoDados.CreateTable<ProgramaEntidade>();
                }

                return bancoDados;
            }
        }
    }
}
