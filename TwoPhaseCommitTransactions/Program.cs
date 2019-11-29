using FirebirdSql.Data.FirebirdClient;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace TwoPhaseCommitTransactions
{
    class Program
    {
        private static StringWriter _writer = new StringWriter();
        static void Main(string[] args)
        {
            try
            {
                var sql = "INSERT INTO TESTDBHELPER (ID, NOME) VALUES(2, 'Daniel Rodrigues da silva')";
                var sb1 = StringDeConexao.Crie(@"C:\work_dotnet_framework\Transactions\TwoPhaseCommitTransactions\Dados\TwoPhaseCommit.FB3");
                var sb2 = StringDeConexao.Crie(@"C:\work_dotnet_framework\Transactions\TwoPhaseCommitTransactions\Dados\BDTwoPhaseCommit.FB3");
                using (var transaction = new TransactionScope())
                {
                    using (var con1 = new FbConnection(sb1.ToString()))
                    {
                        con1.Open();

                        var comand = new FbCommand(sql, con1);
                        var retornoDoComand = comand.ExecuteNonQuery();
                        _writer.WriteLine("Rows to be affected by command1: {0}", retornoDoComand);
                    }

                    using (var con2 = new FbConnection(sb2.ToString()))
                    {
                        con2.Open();

                        var comand = new FbCommand(sql, con2);
                        var retornoDoComand = comand.ExecuteNonQuery();
                        _writer.WriteLine("Rows to be affected by command1: {0}", retornoDoComand);
                    }

                    transaction.Complete();
                }
            }
            catch (Exception ex)
            {
                _writer.WriteLine("TransactionAbortedException Message: {0}", ex.Message);
            }

            Console.WriteLine(_writer.ToString());
            Console.ReadKey();
        }
    }
}
