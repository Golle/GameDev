using System;
using System.Collections.Generic;
using Titan.EntityComponentSystem;
using Titan.EntityComponentSystem.Entities;
using Titan.Systems.EntitySystem;

namespace Titan.Systems.SceneGraph
{
    internal interface ISceneLoader
    {

        IScene Load(string identifier);
        void LoadAsync(string identifier, Action<IScene> callback);
        void Unload(IScene scene);
        void UnloadAsync(IScene scene, Action callback);
    }

    internal interface ISceneManager
    {
        void SetActive(IScene scene);
        void Enable(IScene scene);
        void Disable(IScene scene);
    }


    internal class Scene : IScene
    {
        private IList<Entity> _entities = new List<Entity>(100);

        public Scene(string name)
        {
            
        }
    }

    internal interface IScene
    {
    }
}
