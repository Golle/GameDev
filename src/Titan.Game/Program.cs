using System;
using System.Collections;
using Titan.Core.Ioc;
using Titan.Graphics;
using Titan.Graphics.Blobs;
using Titan.Graphics.Camera;

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
        private ICamera _camera;

        public MyTestClass(ICameraFactory factory)
        {
            _camera = factory.CreateOrhographicCamera();
        }

        public void Print()
        {

            var a = new BitArray(new bool[] {true, false, false});

            var b = new BitArray(new bool[] {true, false, true});

            var c = a.And(b);

            Console.WriteLine("Apa");
        }
    }
}
