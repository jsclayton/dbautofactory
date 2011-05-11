﻿using System;
using Autofac;

namespace DbAutoFactory
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var builder = new ContainerBuilder();
            builder.RegisterType<DummyConfig>().As<IConfig>();
            builder.RegisterType<Database>().As<IDatabase>();
            builder.Register(c => new Foo(c.Resolve<IConfig>().GetDatabase("Foo")));
            builder.Register(c => new Bar(c.Resolve<IConfig>().GetDatabase("Bar")));
            using (var container = builder.Build())
            {
                container.Resolve<Foo>().Write();
                container.Resolve<Bar>().Write();
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
}