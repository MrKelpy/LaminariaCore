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
            
            QueryTest();
            
            int rows = manager.RunSqlScript("../../assets/school.sql");
            Console.WriteLine(rows + " rows affected.");
            Assert.Pass();
        }
        
        [Test]
        public void QueryTest()
        {
            using SQLServerConnector connector = new SQLServerConnector(@".\SQLEXPRESS", "master");
            SQLDatabaseManager manager = new SQLDatabaseManager(connector);

            if (!manager.UseDatabase("Escola")) Assert.Fail();

            var fields = manager.SendQuery("SELECT * FROM Alunos");
            foreach (string[] field in fields)
                Console.WriteLine(field[0] + " " + field[1] + " " + field[2]);
            
            // Assert.Pass();
        }
        
        [Test]
        public void RestrictedInsertionTest()
        {
            SQLServerConnector connector = new SQLServerConnector(@".\SQLEXPRESS", "Escola");
            SQLDatabaseManager manager = new SQLDatabaseManager(connector);

            if (!manager.UseDatabase("Escola")) Assert.Fail();

            string[] fields = { "Nome", "Idade", "Localidade" };
            dynamic[] parameters = { "Ambrósio Oliveira", 18, "Forte da Casa" };
            if (manager.InsertIntoRestricted("Alunos", fields, parameters))
                Assert.Pass();
            
            Assert.Fail();
        }

        [Test]
        public void InsertionTest()
        {
            SQLServerConnector connector = new SQLServerConnector(@".\SQLEXPRESS", "Escola");
            SQLDatabaseManager manager = new SQLDatabaseManager(connector);

            if (!manager.UseDatabase("Escola")) Assert.Fail();

            if (manager.InsertInto("Alunos", "Reinaldo Ferreira", 15, "Forte da Casa"))
                Assert.Pass();
            
            Assert.Fail();
        }

        [Test]
        public void BackupTest()
        {
            SQLServerConnector connector = new SQLServerConnector(@".\SQLEXPRESS", "Escola");
            SQLDatabaseManager manager = new SQLDatabaseManager(connector);
            manager.ExportDatabase("Escola", "C:/dev/escola.bak");
            
            Assert.Pass();
        }
    }
}