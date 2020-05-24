using System;
using System.Collections.Generic;

namespace Titan.Core.Ioc
{
    public interface IContainer
    {
        IContainer Register<TConcrete>() where TConcrete : class;
        IContainer Register<TTypeToResolve, TConcrete>() where TConcrete : TTypeToResolve;
        TTypeToResolve CreateInstance<TTypeToResolve>();
        TTypeToResolve GetInstance<TTypeToResolve>();
        IContainer RegisterSingleton<TTypeToResolve>(TTypeToResolve instance);
        object GetInstance(Type type);
        IContainer AddRegistry<T>() where T : IRegistry;
        IEnumerable<T> GetAll<T>();
        IContainer CreateChildContainer();
    }
}
