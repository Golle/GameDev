using System;
using System.Diagnostics;
using System.Numerics;
using Titan.Core.EventSystem;
using Titan.Core.Logging;
using Titan.D3D11;
using Titan.EntityComponentSystem;
using Titan.EntityComponentSystem.Components;
using Titan.EntityComponentSystem.Entities;
using Titan.EntityComponentSystem.Systems;
using Titan.Graphics.Camera;
using Titan.Graphics.Renderer;
using Titan.Windows.Input;
using Titan.Windows.Window;

namespace Titan
{
    internal class GameEngine
    {
        private readonly IWindow _window;
        private readonly IEventManager _eventManager;
        private readonly ILogger _logger;
        private readonly IInputManager _inputManager;
        private readonly ICameraFactory _cameraFactory;
        private readonly IRenderer _renderer;
        private readonly IEntityManager _entityManager;
        private readonly IComponentManager _componentManager;

        private ICamera _camera;
        private RendereableModel _model;
        public GameEngine(IWindow window, IEventManager eventManager, ILogger logger, IInputManager inputManager, ICameraFactory cameraFactory, IRenderer renderer, IEntityManager entityManager, IComponentManager componentManager, IContext context)
        {
            _window = window;
            _eventManager = eventManager;
            _logger = logger;
            _inputManager = inputManager;
            _cameraFactory = cameraFactory;
            _renderer = renderer;
            _entityManager = entityManager;
            _componentManager = componentManager;


            _componentManager.RegisterComponent<TestComponent1>(10000);
            _componentManager.RegisterComponent<TestComponent2>(10000);


            var testSystem1 = new TestSystem(_componentManager);
            var testSystem2 = new TestSystem(_componentManager);

            context.RegisterSystem(testSystem1);
            context.RegisterSystem(testSystem2);

            
            
            

            //for (int i = 0; i < 2; i++)
            {
                var s = Stopwatch.StartNew();
                var count = 100_00;
                _logger.Debug("Adding 2 systems subscribing to 2 components");
                _logger.Debug("Creating 1000 Entities");
                _logger.Debug("Creating 2000 Components");
                _logger.Debug("Updating systems (Doing 2x Matrix4x4 multiplications");
                _logger.Debug("Destroying 2000 Components");
                _logger.Debug("Destroying 1000 Entities");
                for (var i = 0; i < count; ++i)
                {
                    var entities = CreateEntities();
                    var components = CreateComponents(entities);

                    for (var x = 0; x < 100; ++x)
                    {
                        testSystem1.Update(1.1f);
                        testSystem2.Update(1.1f);
                    }

                    foreach (var component in components)
                    {
                        _componentManager.Free(component);
                    }
                    foreach (var entity in entities)
                    {
                        _entityManager.Free(entity);
                    }
                    //var entity = _entityManager.Create(); 
                    //var testComp1 = _componentManager.Create<TestComponent1>(entity);
                    //var testComp2 = _componentManager.Create<TestComponent2>(entity);
                    //testSystem1.Update(0.1f);
                    //testSystem2.Update(0.1f);
                    //_componentManager.Free(testComp1);
                    //testSystem1.Update(0.1f);
                    //testSystem2.Update(0.1f);
                    //var testComp3 = _componentManager.Create<TestComponent1>(entity);
                    //testSystem1.Update(0.1f);
                    //testSystem2.Update(0.1f);

                    //_componentManager.Free(testComp2);
                    //_componentManager.Free(testComp3);
                    //_entityManager.Free(entity);
                    //testSystem1.Update(0.1f);
                    //testSystem2.Update(0.1f);
                }
                s.Stop();
                Console.WriteLine($"{count} iterations, time elapsed: {s.Elapsed.TotalMilliseconds} ms. {s.Elapsed.TotalMilliseconds/ count} ms/iteration");

                //var entity = _entityManager.Create();

                
                
                //var testComp1 = _componentManager.Create<TestComponent1>(entity);
                //var testComp2 = _componentManager.Create<TestComponent2>(entity);

                //testSystem1.Update(0.1f);
                //testSystem2.Update(0.1f);
                //_componentManager.Free(testComp1);

                //testSystem1.Update(0.1f);
                //testSystem2.Update(0.1f);
                //var testComp3 = _componentManager.Create<TestComponent1>(entity);
                //testSystem1.Update(0.1f);
                //testSystem2.Update(0.1f);
                ////var testComp4 = _componentManager.Create<TestComponent2>(entity);

                //_entityManager.Free(entity);
                //testSystem1.Update(0.1f);
                //testSystem2.Update(0.1f);
            }

            //system.Update(0.1f);
            
            //s.Restart();
            for (var i = 0; i < 1; ++i)
            {
                //system.Update(0.1f);
            }
            
            //s.Stop();
            //Console.WriteLine($"Time elapsed: {s.Elapsed.TotalMilliseconds} ms");


            Setup();
            //entity.Destroy();
        }

        private Component[] CreateComponents(in uint[] entities)
        {
            var components = new Component[entities.Length*2];
            for (var i = 0; i < entities.Length; ++i)
            {
                components[i] = _componentManager.Create<TestComponent1>(entities[i]);
                components[i+entities.Length] = _componentManager.Create<TestComponent2>(entities[i]);
            }
            return components;
        }

        private uint[] CreateEntities()
        {
            var a = new uint[1000];
            for (var i = 0; i < a.Length; ++i)
            {
                a[i] = _entityManager.Create();
            }
            return a;
        }

        private void Setup()
        {
            _camera = _cameraFactory.CreatePerspectiveCamera();
            var vertices = new []
            {
                new Vertex {Color = new Color(1f, 0, 0),  X = -1f, Y = -1f, Z = -1f},
                new Vertex {Color = new Color(1f, 1f, 0), X = 1f, Y = -1f, Z = -1f},
                new Vertex {Color = new Color(1f, 0, 1f), X = -1f, Y = 1f, Z = -1f},
                new Vertex {Color = new Color(0f, 1f, 0), X = 1f, Y = 1f, Z = -1f},
                new Vertex {Color = new Color(0f, 1f, 1f),X = -1f, Y = -1f, Z = 1f},
                new Vertex {Color = new Color(1f, 1f, 0), X = 1f, Y = -1f, Z = 1f},
                new Vertex {Color = new Color(1f, 1f, 1f),X = -1f, Y = 1f, Z = 1f},
                new Vertex {Color = new Color(1f, 0, 0),  X = 1f, Y = 1f, Z = 1f},
            };
            var indices = new short[]
            {
                0,2,1,  2,3,1,
                1,3,5,  3,7,5,
                2,6,3,  3,6,7,
                4,5,7,  4,7,6,
                0,4,2,  2,4,6,
                0,1,4,  1,5,4
            };
            _model = new RendereableModel(vertices, indices, Vector3.Zero);
            
        }

        public bool Execute()
        {
            var mousePosition = _inputManager.Mouse.Position;
            _window.SetTitle($"x: {mousePosition.X} y: {mousePosition.Y}");
            

            if (!_window.Update())
            {
                _logger.Debug("Window kill commands received, exiting Engine.");
                return false;
            }
            _eventManager.Update();

            // Update physics?

            // Execute queued sounds


            // Draw 3D World
            // Draw the UI/Hud
            _renderer.Push(_model);
            _renderer.Flush();


            _camera.RotateX((-_inputManager.Mouse.Position.Y/(float)_window.Width) * 2f);
            _camera.RotateZ((_inputManager.Mouse.Position.X/ (float)_window.Height) * 2f);

            
            _renderer.BeginScene(_camera);

            

            _renderer.EndScene();

            return true;
        }
    }
}
