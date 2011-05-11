using System;
using Autofac;

namespace DbAutoFactory
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            // http://code.google.com/p/autofac/wiki/DelegateFactories
            // If type T is registered with the container, Autofac will automatically resolve dependencies 
            // on Func<T> as factories that create T instances through the container.
            var builder = new ContainerBuilder();
            builder.RegisterType<DummyConfig>().As<IConfig>();
            builder.RegisterType<Database>().As<IDatabase>();
            builder.Register(c => new Foo(c.Resolve<IConfig>().GetDatabase("Foo")));
            builder.Register(c => new Bar(c.Resolve<IConfig>().GetDatabase("Bar")));
            builder.RegisterType<Lorem>();
            builder.RegisterType<Ipsum>();
            using (var container = builder.Build())
            {
                container.Resolve<Foo>().Write();
                container.Resolve<Bar>().Write();
                var lorem = container.Resolve<Lorem>();
                var ipsum = container.Resolve<Ipsum>();
            }

            Console.ReadLine();
        }
    }

    internal class Foo
    {
        private readonly IDatabase database;

        public Foo(IDatabase database)
        {
            this.database = database;
        }

        public void Write()
        {
            Console.Write("Foo is writing => ");
            database.Write();
        }
    }

    internal class Bar
    {
        private readonly IDatabase database;

        public Bar(IDatabase database)
        {
            this.database = database;
        }

        public void Write()
        {
            Console.Write("Bar is writing => ");
            database.Write();
        }
    }

    internal class Lorem
    {
        private readonly DatabaseFactory databaseFactory;

        public Lorem(DatabaseFactory databaseFactory)
        {
            this.databaseFactory = databaseFactory;
        }
    }

    internal class Ipsum
    {
        private readonly IConfig config;

        public Ipsum(IConfig config)
        {
            this.config = config;
        }
    }
}