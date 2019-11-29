using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FirebirdSql.Data.FirebirdClient;

namespace TwoPhaseCommitTransactions
{
    public static class StringDeConexao
    {
        public static FbConnectionStringBuilder Crie(string conexaoDatabase)
        {//C:\work_dotnet_framework\Transactions\TwoPhaseCommitTransactions\Dados\TwoPhaseCommit.FB3
            return new FbConnectionStringBuilder
            {
                Database = conexaoDatabase,
                DataSource = "localhost",
                UserID = "SYSDBA",
                Password = "masterkey",
                Pooling = true,
                PacketSize = 32000,
                FetchSize = 32000,
                Port = 3053
            };
        }
    }
}
