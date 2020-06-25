using System;
using System.Collections.Generic;
using System.IO;
using System.Numerics;
using Titan.Core.Assets.WavefrontObj.Parsers;
using Titan.Core.Logging;

namespace Titan.Core.Assets.WavefrontObj
{
    internal class ObjLoader : IObjLoader
    {
        private readonly IVertexParser _vertexParser;
        private readonly INormalParser _normalParser;
        private readonly IFaceParser _faceParser;
        private readonly ITextureParser _textureParser;
        private readonly ILogger _logger;

        public ObjLoader(IVertexParser vertexParser, INormalParser normalParser, IFaceParser faceParser, ITextureParser textureParser, ILogger logger)
        {
            _textureParser = textureParser;
            _logger = logger;
            _faceParser = faceParser;
            _normalParser = normalParser;
            _vertexParser = vertexParser;
        }

        public WavefrontObject LoadFromFile(string filename)
        {
            _logger.Debug("Loading Obj file from {0}", filename);
            using var file = File.OpenText(filename);
            
            var vertices = new List<Vector3>();
            var normals = new List<Vector3>();
            var textures = new List<Vector2>();
            var faces = new List<Face>();

            while (TryParse(file, out var objLine))
            {
                switch (objLine.Type)
                {
                    case ObjTypes.Vertex: vertices.Add(_vertexParser.Parse(objLine.Value));break;
                    case ObjTypes.Normal: normals.Add(_normalParser.Parse(objLine.Value));break;
                    case ObjTypes.Texture: textures.Add(_textureParser.Parse(objLine.Value));break;
                    case ObjTypes.Face: faces.AddRange(_faceParser.Parse(objLine.Value));break;
                    //TODO: Add support for Objects, Groups, Materials, Lights
                    default: 
                        _logger.Debug("Unknown type: {0} value: {1}", objLine.Type, objLine.Value);
                        break;
                }
            }
            _logger.Debug("Finished loading model. Vertices: {0}, Textures: {1}, Normals: {2}, Faces: {3}", vertices.Count, textures.Count, normals.Count, faces.Count);
            return new WavefrontObject(vertices.ToArray(), normals.ToArray(), textures.ToArray(), faces.ToArray());
        }

        private static bool TryParse(StreamReader stream, out ObjLine line)
        {
            while (!stream.EndOfStream)
            {
                var result = stream.ReadLine();
                if (string.IsNullOrWhiteSpace(result) || result[0] == '#')
                {
                    continue;
                }
                var splitLine = result
                    .Trim()
                    .Split(' ', 2);
                var type = splitLine[0] switch
                {
                    "v" => ObjTypes.Vertex,
                    "vt" => ObjTypes.Texture,
                    "vn" => ObjTypes.Normal,
                    "o" => ObjTypes.Object,
                    "f" => ObjTypes.Face,
                    "usemtl" => ObjTypes.UseMaterial,
                    "s" => ObjTypes.Smoothing,
                    "mtllib" => ObjTypes.MaterialLibrary,
                    _ => ObjTypes.Unknown
                };
                line = new ObjLine(type, splitLine[1]);
                return true;
            }
            line = default;
            return false;
        }
    }
}
