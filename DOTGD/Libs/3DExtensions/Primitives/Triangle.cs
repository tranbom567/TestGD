using DOTGD.Libs._3DExtensions.Rendering;
using DOTGD.Libs.DotPieTriggers;
using DotPieGDLib;
using GeometryDashAPI.Levels;
using GeometryDashAPI.Levels.GameObjects.Default;
using GeometryDashAPI.Levels.Structures;
using GlmSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DOTGD.Libs._3DExtensions.Primitives
{
    internal class Triangle
    {
        public vec3 p1, p2, p3;
        public virtual vec3 calculateSurfaceNormal()
        {

            return vec3.Cross(p2-p1,p3-p1).Normalized;
        }
        public RgbColor manipulateLightingColor(mat4 modelMatrix,RgbColor objectColor,vec3 lightDirection)
        {
            var colorVec4 = new vec4(objectColor.Red / 255, objectColor.Green / 255, objectColor.Blue / 255, 1);
            var surfaceNormal = calculateSurfaceNormal();
            Console.WriteLine($"Normal {surfaceNormal}");

            var toWorld = modelMatrix.transformPosition(new vec4(surfaceNormal, 0));
            Console.WriteLine($"World {toWorld}");
            var difuseFactor = vec4.Dot(toWorld, -new vec4(lightDirection, 0));
            var calculateLighting = colorVec4 * glm.Clamp(difuseFactor + .25f, 0, 1);
            Console.WriteLine($"difuse: {glm.Clamp(difuseFactor + .25f, 0, 1)}, {calculateLighting}");
            return new RgbColor(Convert.ToByte(calculateLighting.x * 255), Convert.ToByte(calculateLighting.y * 255), Convert.ToByte(calculateLighting.z * 255));
        }
        
        public virtual void draw(ExtendedBlock bl, ExtendedBlock br, ExtendedBlock tl, ExtendedBlock tr,vec3 lightDirection, RgbColor objectColor, float width,float height,mat4 modelMatrix,mat4 ProjectMatrix,Level levelIns)
        {
            
            

            //project to screen
            var transP1 = mat4.Project(p1, modelMatrix, ProjectMatrix,new vec4(0,0,width,height));
            var transP2 = mat4.Project(p2, modelMatrix, ProjectMatrix, new vec4(0, 0, width, height));
            var transP3 = mat4.Project(p3, modelMatrix, ProjectMatrix, new vec4(0, 0, width, height));

            //init data blocks
            bl.PositionX = transP1.x;
            bl.PositionY = transP1.y;
            bl.Groups = new int[] { Data.freeID };
            bl.ColorBase = 50;

            br.PositionX = transP2.x;
            br.PositionY = transP2.y;
            br.Groups = new int[] { Data.freeID+1 };
            bl.ColorBase = 50;

            tl.PositionX = transP3.x;
            tl.PositionY = transP3.y;
            tl.Groups = new int[] { Data.freeID +2};
            bl.ColorBase = 50;

            levelIns.AddBlock(bl);
            levelIns.AddBlock(br);
            levelIns.AddBlock(tl);

            //setting gradient
            Color gdColor = new Color(Data.freeColor);

           
            gdColor.Rgb = manipulateLightingColor(modelMatrix,objectColor,lightDirection);
            levelIns.AddColor(gdColor);
            Gradient drawGrad = new Gradient()
            {
                bl = Data.freeID,
                br = Data.freeID+1,
                tl = Data.freeID+2,
                tr = Data.freeID,
                ColorBase = (short)Data.freeColor,
                Color2 = (short)Data.freeColor,
                PositionX = (transP1.z + transP2.z + transP3.z) / 3,
                id=Data.freeGradId,
                vertexMode=true,
            };
            Data.freeColor++;
            Data.freeGradId++;
            levelIns.AddBlock(drawGrad);

        }
    }
}
