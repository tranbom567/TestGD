using DOTGD.Libs._3DExtensions.Primitives;
using GlmSharp;
using ObjLoader.Loader.Data;
using ObjLoader.Loader.Data.Elements;
using ObjLoader.Loader.Data.VertexData;
using ObjLoader.Loader.Loaders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DOTGD.Libs._3DExtensions.Rendering
{
    internal class LoadModel
    {
        private IObjLoader objLoad = new ObjLoaderFactory().Create();
        public LoadResult result;
        public List<DOTGD.Libs._3DExtensions.Primitives.Triangle> faces;
        public List<vec3> vertices;
        public LoadModel(string modelFile)
        {
            result = objLoad.Load(new FileStream(modelFile, FileMode.Open, FileAccess.Read));
            Console.WriteLine("Load file completed!");
            faces = new List<Triangle>();
            vertices = new List<vec3>();
            UpdateVertices();
            UpdateMeshes();
        }
        public void UpdateMeshes()
        {
            result.Groups.AsParallel().ForAll(ModelProcess);
            Console.WriteLine("update meshes");
        }
        public void UpdateVertices() {
            Console.WriteLine($"{result.Vertices.Count} vertices");
            foreach (Vertex vec in result.Vertices) {
                vertices.Add(new vec3(vec.X, vec.Y, vec.Z));
            }

            
            Console.WriteLine("Reading vertices");
        }
        public void ModelProcess(Group groupf) {
            groupf.Faces.AsParallel().ForAll(faceProcess);
        }
        public void faceProcess(Face facef)
        {
            if (facef.Count == 4)
            {
                
                faces.Add(new Quad()
                {
                    p1 = vertices[facef[0].VertexIndex - 1],
                    p2 = vertices[facef[1].VertexIndex - 1],
                    p3 = vertices[facef[2].VertexIndex - 1],
                    p4 = vertices[facef[3].VertexIndex - 1]
                });
                Console.WriteLine("Add quad!");
            }
            else if (facef.Count == 3) {
                faces.Add(new DOTGD.Libs._3DExtensions.Primitives.Triangle()
                {
                    p1 = vertices[facef[0].VertexIndex - 1],
                    p2 = vertices[facef[1].VertexIndex - 1],
                    p3 = vertices[facef[2].VertexIndex - 1]
                });
                Console.WriteLine("Add triangle!");
            }
        }
    }
}
