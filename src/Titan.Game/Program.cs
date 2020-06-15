using System;
using System.Diagnostics;
using System.Numerics;
using Titan.Components;
using Titan.Core.Ioc;
using Titan.ECS.World;
using Titan.ECS2;
using Titan.ECS2.Components;
using Titan.ECS2.Entities;
using Titan.ECS2.Systems;
using Titan.ECS3;

namespace Titan.Game
{

    
    internal class Program : Application<Program>
    {


        private MyTestClass _test;
        static void Main(string[] args)
        {
            new EntityTestClass().Run();


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
