using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Threading.Tasks;

namespace EnsureConnection
{
    class Program
    {

        static void Main(string[] args)
        {
            char command = 'n';
            do
            {
                string keyList = System.Configuration.ConfigurationManager.AppSettings["connectionStringList"];
                var keyArray = keyList.Split(',');
                Console.Clear();
                Connection c = new Connection();
                foreach (var item in keyArray)
                {
                    c.EnsureConnection(System.Configuration.ConfigurationManager.AppSettings[item]);
                }
                Console.WriteLine("Do you want to try again (Y/N) ? ");
                command = Console.ReadKey(true).KeyChar;
            } while (command == 'y' || command == 'Y');
            Console.WriteLine("Press any key to exit.....");
            Console.ReadKey();
        }

        class Connection
        {
            public string EnsureConnection(string ConnectionString)
            {
                string ret = "Unable to connect : " + ConnectionString;
                Console.WriteLine();
                Console.Write("Trying to connect: " + ConnectionString);
                try
                {
                    using (SqlConnection con = new SqlConnection(ConnectionString))
                    {
                        con.Open();
                        Console.Write("==>Successfully Connected");
                        ret = "Connection successful : " + ConnectionString;
                        con.Close();
                        Console.WriteLine();
                    }
                }
                catch (Exception ex)
                {
                    Console.Write("==>Unable to connect");
                    Console.WriteLine(ex.Message);
                    Console.WriteLine();
                }

                return ret;
            }
        }
    }
}
