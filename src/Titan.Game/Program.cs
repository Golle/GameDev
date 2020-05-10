using System;
using Titan.Core.Ioc;

namespace Titan.Game
{
    internal class Program : Application<Program>
    {
        static void Main(string[] args)
        {
            Start();
        }

        protected override void OnStart()
        {
            GetInstance<MyTestClass>()
                .Print();

        }

        protected override void OnQuit()
        {
            GetInstance<MyTestClass>()
                .Print();
        }

        protected override void RegisterServices(IContainer container)
        {
            container.Register<MyTestClass>();

        }
    }

    internal class MyTestClass
    {
        public void Print()
        {
            Console.WriteLine("Apa");
        }
    }
}
