// See https://aka.ms/new-console-template for more information
using System;
using System.Data;
using System.Diagnostics;
using System.Threading.Tasks;
using Kudos.DataBasing.Drivers;
using Kudos.DataBasing.Enums;
using Kudos.DataBasing.Results;
using Kudos.Dating.Types;
using MySql.Data.MySqlClient;

namespace Kudos.DataBasing.UnitTest
{
    public class EntryPoint
    {
        public static async Task Main(String[] args)
        {
            MySQLDataBaseDriver
                dbd =
                    KudosDataBasing
                        .RequestMySQLDataBaseDriverBuilder()
                            .SetCommandTimeout(30)
                            .SetUserName("arteideuser")
                            .SetUserPassword("7YgTpwniTe4XdHH3£.")
                            .SetSchemaName("arteidedb")
                            .SetSessionPoolTimeout(30)
                            .IsConnectionResetEnabled(true)
                            .IsSessionPoolInteractive(false)
                            .SetCharacterSet(EDataBaseCharacterSet.utf8mb4)
                            .SetHost("3.124.31.6")
                            .SetPort(2024)
                            .SetConnectionProtocol(MySqlConnectionProtocol.Socket)
                            .Build();

            await dbd.OpenConnectionAsync();

            DataBaseQueryResult
                prvoa = 
            await
                dbd.RequestExecutor()
                    .SetCommandType(CommandType.Text)
                    .SetCommandText("SELECT * FROM arteidedb.tblAAAA WHERE iAAAAID = @iAAAAID")
                    .AddParameter("@iAAAAID", 10)// WHERE iClnID = @iClnID AND sClnFirstName = @sClnFirstName")
                    .ExecuteAsQueryAsync();

            if(prvoa.HasDataTable)
            {
                Stopwatch sw = new Stopwatch();

                sw.Restart();

                DataColumn dc;
                for(int j=0; j<prvoa.DataTable.Columns.Count; j++)
                {
                    dc = prvoa.DataTable.Columns[j];
                    Object? o;
                    for (int i=0; i<prvoa.DataTable.Rows.Count; i++)
                    {
                        o = prvoa.DataTable.Rows[i][dc];
                    }
                }

                sw.Stop();

                Console.WriteLine("Elapsed " + sw.Elapsed);

                sw.Restart();

                FastDataTableReader fdtr = FastDataTableReader.New(prvoa.DataTable);

                DataColumn dc0;
                for (int j = 0; j < fdtr.ColumnsReader.Columns.Count; j++)
                {
                    dc0 = fdtr.ColumnsReader[j];
                    Object? o1;
                    for (int i = 0; i < prvoa.DataTable.Rows.Count; i++)
                    {
                        o1 = fdtr.RowsReader[i][dc0];
                    }
                }

                sw.Stop();

                Console.WriteLine("Elapsed " + sw.Elapsed);
            }
            Console.WriteLine("Hello, World!");
        }
    }
}