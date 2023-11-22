using System;
using LaminariaCore_Databases.enumeration;
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
            using SQLServerConnector connector = new SQLServerConnector(@".\SQLEXPRESS", "master");
            SQLDatabaseManager manager = new SQLDatabaseManager(connector);
            
            int rows = manager.RunSqlScript("../../assets/school.sql");
            Console.WriteLine(rows + " rows affected.");
            Assert.Pass();
        }
        
        [Test]
         public void ViewTest()
        {
            using SQLServerConnector connector = new SQLServerConnector(@".\SQLEXPRESS", "master");
            SQLDatabaseManager manager = new SQLDatabaseManager(connector);
            
            int rows = manager.RunSqlScript("../../assets/school.sql");
            Console.WriteLine(rows + " rows affected.");

            manager.UseDatabase("Escola");
            string viewName = "nums";
            manager.CreateView(viewName, new [] {"Alunos.NumeroInterno"}, new [] {"Alunos"});

            foreach (var array in manager.Select(viewName)) {
                Console.WriteLine(array[0]);
            }
            
            Assert.Pass();
        }
        
        [Test]
        public void QueryTest()
        {
            using SQLServerConnector connector = new SQLServerConnector(@".\SQLEXPRESS", "master");
            SQLDatabaseManager manager = new SQLDatabaseManager(connector);

            if (!manager.UseDatabase("Escola")) Assert.Fail();

            var fields = manager.SendQuery("SELECT Nome, Localidade FROM [Alunos] INNER JOIN [Matriculas] ON [Alunos].NumeroInterno = [Matriculas].NumeroInterno WHERE Ano = 11;");
            foreach (string[] field in fields)
                Console.WriteLine(field[0] + " " + field[1]);
            
            Assert.Pass();
        }
        
        [Test]
        public void DeleteFromTest()
        {
            ScriptTest();
            
            using SQLServerConnector connector = new SQLServerConnector(@".\SQLEXPRESS", "Escola");
            SQLDatabaseManager manager = new SQLDatabaseManager(connector);

            if (!manager.UseDatabase("Escola")) Assert.Fail();

            int rows = manager.DeleteFrom("TurmasDisciplinas", "CodigoDisciplina = 3");
                
            var results = manager.SendQuery("SELECT * FROM [TurmasDisciplinas]");
            foreach (string[] result in results)
                Console.WriteLine(result[0] + " " + result[1] + " " + result[2]);
            
            Console.WriteLine(rows + " rows affected.");
            if (rows > 0) Assert.Pass();
            Assert.Fail();
        }

        [Test]
        public void UpdateAndSelectTest()
        {
            ScriptTest();
            
            using SQLServerConnector connector = new SQLServerConnector(@".\SQLEXPRESS", "Escola");
            SQLDatabaseManager manager = new SQLDatabaseManager(connector);
            
            if (!manager.UseDatabase("Escola")) Assert.Fail();
            
            int rows = manager.Update("Alunos", "Nome", "Reinaldo Ferreira", "Idade = 17");
            var results = manager.Select(new [] {"NumeroInterno", "Nome"}, "Alunos");
            
            foreach (string[] result in results)
                Console.WriteLine(result[0] + " " + result[1]);
            
            Console.WriteLine(rows + " rows affected.");

            Assert.Pass();
        }
        
        [Test]
        public void RestrictedInsertionTest()
        {
            using SQLServerConnector connector = new SQLServerConnector(@".\SQLEXPRESS", "Escola");
            SQLDatabaseManager manager = new SQLDatabaseManager(connector);

            if (!manager.UseDatabase("Escola")) Assert.Fail();

            string[] fields = { "Nome", "Idade", "Localidade" };
            
            if (manager.InsertInto("Alunos", fields,  "Ambrósio Oliveira", 18, "Forte da Casa"))
                Assert.Pass();
            
            Assert.Fail();
        }

        [Test]
        public void InsertionTest()
        {
            using SQLServerConnector connector = new SQLServerConnector(@".\SQLEXPRESS", "Escola");
            SQLDatabaseManager manager = new SQLDatabaseManager(connector);

            if (!manager.UseDatabase("Escola")) Assert.Fail();

            if (manager.InsertInto("Alunos", "Reinaldo Ferreira", 15, "Forte da Casa"))
                Assert.Pass();
            
            Assert.Fail();
        }

        [Test]
        public void BackupTest()
        {
            using SQLServerConnector connector = new SQLServerConnector(@".\SQLEXPRESS", "Escola");
            SQLDatabaseManager manager = new SQLDatabaseManager(connector);
            manager.ExportDatabase("Escola", "C:/dev/escola.bak");
            
            Assert.Pass();
        }
        
        [Test]
        public void RestoreTest()
        {
            using SQLServerConnector connector = new SQLServerConnector(@".\SQLEXPRESS", "master");
            SQLDatabaseManager manager = new SQLDatabaseManager(connector);
            
            manager.ImportDatabase("Escola", "C:/dev/escola.bak");
            
            Assert.Pass();
        }

        [Test]
        public void SelectTest()
        {
            using SQLServerConnector connector = new SQLServerConnector(@".\SQLEXPRESS", "Escola");
            SQLDatabaseManager manager = new SQLDatabaseManager(connector);

            foreach (var array in manager.Select("Alunos")) {
                Console.WriteLine(array[0] + " " + array[1] + " " + array[2]);
            }
            
            foreach (var array in manager.Select("Alunos", SQLQueryOptions.IncludeHeaders)) {
                Console.WriteLine(array[0] + " " + array[1] + " " + array[2]);
            }
            
            Assert.Pass();
        }
        
        [Test]
        public void ConnectionTest()
        {
            using SQLServerConnector connector = new SQLServerConnector(@".\SQLEXPRESS", "Escola");
            SQLDatabaseManager manager = new SQLDatabaseManager(connector);
            
            // Disconnects and reconnects multiple times
            for (int i = 0; i < 10; i++) {
                manager.Connector.Disconnect();
                Console.WriteLine("Disconnected.");
                manager.Connector.Reconnect();
                Console.WriteLine("Reconnected.");
            }
            
            foreach (var array in manager.Select("Alunos")) {
                Console.WriteLine(array[0] + " " + array[1] + " " + array[2]);
            }
            
            foreach (var array in manager.Select("Alunos", SQLQueryOptions.IncludeHeaders)) {
                Console.WriteLine(array[0] + " " + array[1] + " " + array[2]);
            }
            
            Assert.Pass();
        }
    }
}