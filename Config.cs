using System;
using System.Collections.Generic;

namespace DbAutoFactory
{
    public interface IConfig
    {
        IDatabase GetDatabase(string name);
    }

    public class DummyConfig : IConfig
    {
        private readonly DatabaseFactory databaseFactory;
        private readonly IDictionary<string, string> connectionStrings;

        public DummyConfig(DatabaseFactory databaseFactory)
        {
            this.databaseFactory = databaseFactory;
            connectionStrings = new Dictionary<string, string>(StringComparer.CurrentCultureIgnoreCase)
                {
                    { "Mine", "Mine Connection String" }, 
                    { "ActivityLogging", "Activity Logging Connection String"},
                    { "VerticalGoods", "Vertical Goods Connection String"},
                    { "Foo", "Foo Connection String"},
                    { "Bar", "Bar Connection String"}
                };
        }

        public IDatabase GetDatabase(string name)
        {
            string connectionString;
            if (!connectionStrings.TryGetValue(name, out connectionString))
                throw new ArgumentException(string.Format("Connection string does not exist: {0}", name), "name");

            return databaseFactory.Invoke(name, connectionString);
        }
    }
}