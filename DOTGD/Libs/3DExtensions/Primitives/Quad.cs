using DOTGD.Libs._3DExtensions.Primitives;
using DOTGD.Libs._3DExtensions.Rendering;
using DOTGD.Libs.DotPieTriggers;
using DotPieGDLib;
using GeometryDashAPI.Levels;
using GeometryDashAPI.Levels.Structures;
using GlmSharp;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace DOTGD.Libs._3DExtensions
{
    internal class Quad : DOTGD.Libs._3DExtensions.Primitives.Triangle
    {
        public vec3 p4;
        public override void draw(ExtendedBlock bl, ExtendedBlock br, ExtendedBlock tl, ExtendedBlock tr, vec3 lightDirection, RgbColor objectColor, float width, float height, mat4 modelMatrix, mat4 projectMatrix, Level levelIns)
        {
            Console.WriteLine("Draw Quad");



            //transform points
            var transP1 = mat4.Project(p1, modelMatrix, projectMatrix, new vec4(0, 0, width, height));
            var transP2 = mat4.Project(p2, modelMatrix, projectMatrix, new vec4(0, 0, width, height));
            var transP3 = mat4.Project(p3, modelMatrix, projectMatrix, new vec4(0, 0, width, height));
            var transP4 = mat4.Project(p4, modelMatrix, projectMatrix, new vec4(0, 0, width, height));

            Console.WriteLine("Transform points");
            Console.WriteLine($"p1 {p1} -> {transP1}");
            Console.WriteLine($"p2 {p2} -> {transP2}");
            Console.WriteLine($"p3 {p3} -> {transP3}");
            Console.WriteLine($"p4 {p4} -> {transP4}");

            //Making point in bl-br-tr-tl
            List<vec3> vertices = new List<vec3>([transP1, transP2, transP3, transP4]);
            var ordered = vertices.OrderBy(vec => vec.x).OrderBy(vec => vec.y).ToList();
            Console.WriteLine("Ordered points");
            Console.WriteLine($"p1 {ordered[0]} tl");
            Console.WriteLine($"p2 {ordered[1]} bl");
            Console.WriteLine($"p3 {ordered[2]} tr");
            Console.WriteLine($"p4 {ordered[3]} br");

            //init data blocks
            bl.PositionX = ordered[1].x;
            bl.PositionY = ordered[1].y;
            bl.Groups = new int[] { Data.freeID };

            br.PositionX = ordered[3].x;
            br.PositionY = ordered[3].y;
            br.Groups = new int[] { Data.freeID + 1 };

            tl.PositionX = ordered[0].x;
            tl.PositionY = ordered[0].y;
            tl.Groups = new int[] { Data.freeID + 2 };

            tr.PositionX = ordered[2].x;
            tr.PositionY = ordered[2].y;
            tr.Groups = new int[] { Data.freeID + 3 };

            levelIns.AddBlock(bl);
            levelIns.AddBlock(br);
            levelIns.AddBlock(tl);
            levelIns.AddBlock(tr);
            //setting gradient
            Color gdColor = new Color(Data.freeColor);

            gdColor.Rgb = manipulateLightingColor(modelMatrix, objectColor, lightDirection);
            levelIns.AddColor(gdColor);
            Gradient drawGrad = new Gradient()
            {
                bl = Data.freeID,
                br = Data.freeID + 1,
                tl = Data.freeID + 2,
                tr = Data.freeID + 3,
                ColorBase = (short)Data.freeColor,
                Color2 = (short)Data.freeColor,
                PositionX = (transP1.z + transP2.z + transP3.z) / 3,
                id = Data.freeGradId,
                vertexMode = true,
            };
            Data.freeColor++;
            Data.freeGradId++;
            levelIns.AddBlock(drawGrad);

        }
    }
}
