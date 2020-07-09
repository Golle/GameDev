using System;
using Titan.Core.Assets.WavefrontObj;
using Titan.Core.Ioc;

namespace Titan.Game
{

    struct MyComponent
    {
        
    }
    internal class Program : Application<Program>
    {
        private MyTestClass _test;

        static void Main(string[] args)
        {
            Start();
        }

        protected override void OnInitialize(IContainer container)
        {
            _test = container.GetInstance<MyTestClass>();

        }

        protected override void OnStart()
        {
            _test.OnStart();
        }

        protected override void OnQuit()
        {
            _test.OnQuit();
        }

        protected override void RegisterServices(IContainer container)
        {
            container.Register<MyTestClass>();
        }
    }

    internal class MyTestClass
    {
        public void OnStart()
        {
            
            

            Console.WriteLine("OnStart");
        }

        public void OnQuit()
        {
            Console.WriteLine("OnQuit");
        }
    }
}
