using FirebirdSql.Data.FirebirdClient;
using System;

namespace EMRepository
{
    public class Banco
    {
        private static string pUser = "SYSDBA";
        private static string pPassword = "masterkey";
        private static string pDatabase = "localhost:D:\\Ambiente_Desenvolvimento\\Aulas_C#\\ProjetoEM\\Dados\\DBESCOLAR402.FB4";
        private static string pDataSource = "localhost";
        private static int pPort = 3054;
        private static int pDialet = 3;
        private static string pCharset = FbCharset.Utf8.ToString();

        public FbConnection connection;

        public bool bconexao { get; set; }

        public Banco()
        {
            FbConnectionStringBuilder stringconnection = new FbConnectionStringBuilder()
            {
                Port = pPort,
                UserID = pUser,
                Password = pPassword,
                Database = pDatabase,
                Dialect = pDialet,
                Charset = pCharset
            };

            try
            {
                connection = new FbConnection(stringconnection.ToString());
                connection.Open();
                bconexao = true;
            }
            catch (Exception ex)
            {
                bconexao = false;
            }
            

        }
    }
}
