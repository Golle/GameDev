﻿using System.Collections.Generic;

namespace Titan.Tools.AssetsBuilder.WavefrontObj
{
    public class ObjGroup
    {
        public IList<ObjFace> _faces = new List<ObjFace>();
        public string Name { get; }
        public ObjGroup(string name = null)
        {
            Name = name;
        }

        public void AddFace(ObjFace face)
        {
            _faces.Add(face);
        }
    }
}