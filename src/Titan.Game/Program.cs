using System;
using System.Collections;
using System.Diagnostics;
using Titan.Core.Ioc;
using Titan.ECS.World;
using Titan.Graphics.Camera;

namespace Titan.Game
{
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
        
        public MyTestClass(IWorldCreator worldCreator)
        {
            
        }

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
