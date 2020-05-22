using System;
using System.Collections.Generic;

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
        private IList<uint> _entities = new List<uint>(100);

        public Scene(string name)
        {
            
        }
    }

    internal interface IScene
    {
    }
}
