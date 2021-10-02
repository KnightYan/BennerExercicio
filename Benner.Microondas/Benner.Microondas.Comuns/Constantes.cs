using System;
using System.IO;

namespace Benner.Microondas.Comuns
{
    public static class Constantes
    {
        #region "Banco de Dados"
        public const string NomeBanco = "MicroondasDb.db";

        public const SQLite.SQLiteOpenFlags Flags =
            // open the database in read/write mode
            SQLite.SQLiteOpenFlags.ReadWrite |
            // create the database if it doesn't exist
            SQLite.SQLiteOpenFlags.Create |
            // enable multi-threaded database access
            SQLite.SQLiteOpenFlags.SharedCache;

        public static string CaminhoBanco
        {
            get
            {
                var basePath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
                return Path.Combine(basePath, NomeBanco);
            }
        }

        #endregion

        #region "Exceptions"
        public static string ValorInvalidoException = "Valor '{0}' inválido para o campo: {1}";
        public static string ParametroNullException = "Parâmetro {0} null";
        #endregion
    }
}
