using System;
using System.Threading;
using LaminariaCore_Databases.sqlserver;
using NUnit.Framework;

namespace UnitTesting
{
    [TestFixture]
    public class Tests
    {
        

        [Test]
        public void ScriptTest()
        {
            SQLServerConnector connector = new SQLServerConnector(@".\SQLEXPRESS", "master");
            SQLDatabaseManager manager = new SQLDatabaseManager(connector);
            
            int rows = manager.RunSqlScript("../../assets/school.sql");
            Console.WriteLine(rows + " rows affected.");
            Assert.Pass();
        }
        
        [Test]
        public void QueryTest()
        {
            SQLServerConnector connector = new SQLServerConnector(@".\SQLEXPRESS", "Escola");
            SQLDatabaseManager manager = new SQLDatabaseManager(connector);

            if (!manager.UseDatabase("Escola")) Assert.Fail();

            var fields = manager.SendQuery("SELECT * FROM Alunos");
            foreach (string[] field in fields)
                Console.WriteLine(field[0], field[1], field[2]);

            Assert.Pass();
        }

        [Test]
        public void BackupTest()
        {
            SQLServerConnector connector = new SQLServerConnector(@".\SQLEXPRESS", "Escola");
            SQLDatabaseManager manager = new SQLDatabaseManager(connector);
            int rows = manager.ExportDatabase("Escola", "./backup.bak");
            
            Console.WriteLine(rows + " rows affected.");
        }
    }
}