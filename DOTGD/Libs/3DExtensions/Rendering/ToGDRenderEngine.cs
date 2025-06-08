using DOTGD.Libs._3DExtensions.Primitives;
using DotPieGDLib;
using GeometryDashAPI.Levels;
using GeometryDashAPI.Levels.Structures;
using GlmSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace DOTGD.Libs._3DExtensions.Rendering
{
    internal class ToGDRenderEngine
    {
        
        
        public static void Render(RgbColor objectColor,vec3 lightDirection,Level levelInstance,LoadModel model,mat4 modelMatrix,mat4 projectMatrix,float width,float height)
        {
            //var projectedModel = Matrix4x4.CreatePerspectiveFieldOfView(fov, 1920 / 1080, .2f, 1000);
            foreach (Triangle poly in model.faces)
            {
                faceProcess(objectColor,lightDirection,poly, modelMatrix,projectMatrix,width,height, levelInstance);
            }
        }
        public static void faceProcess(RgbColor objectColor,vec3 lightDirection,Primitives.Triangle poly, mat4 modelMatrix, mat4 projectMatrix, float width, float height, Level levelIns)
        {
            List<ExtendedBlock> blocks = new List<ExtendedBlock>();
            blocks.AddRange([new ExtendedBlock(1) { EditorL=1}, new ExtendedBlock(1) { EditorL = 1 }, new ExtendedBlock(1) { EditorL = 1 }, new ExtendedBlock(1) { EditorL = 1 }]);
            poly.draw(blocks[0], blocks[1], blocks[2], blocks[3],lightDirection,objectColor,width,height,modelMatrix,projectMatrix, levelIns);
            if (poly is Quad)
            {
                Data.freeID += 4;
            }else if(poly is Primitives.Triangle)
            {
                Data.freeID += 3;
            }
        }
    }
}
