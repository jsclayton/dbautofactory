using System;

namespace DbAutoFactory
{
    public delegate IDatabase DatabaseFactory(string name, string connectionString);

    public interface IDatabase : IDisposable
    {
        string Name { get; set; }
        void Read();
        void Write();
    }

    public class Database : IDatabase
    {
        public Database(string name, string connectionString)
        {
            if (String.IsNullOrEmpty(name)) throw new ArgumentNullException("name");
            if (String.IsNullOrEmpty(connectionString)) throw new ArgumentNullException("connectionString");

            Name = name;
            ConnectionString = connectionString;
        }

        public string ConnectionString { get; set; }

        public string Name { get; set; }

        public void Read()
        {
            Console.WriteLine("Reading from {0} ({1})", Name, ConnectionString);
        }

        public void Write()
        {
            Console.WriteLine("Writing to {0} ({1})", Name, ConnectionString);
        }

        public void Dispose()
        {
            Console.WriteLine("Disposing {0} ({1})", Name, ConnectionString);
        }
    }
}