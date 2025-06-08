using GlmSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DOTGD.Libs
{
    internal static class Calculating
    {
        public static vec4 transformPosition(this mat4 mat,vec4 vec)
        {
            return new vec4(
                vec4.Dot(vec.xyzw, mat.Row0.xyzw),
                vec4.Dot(vec.xyzw, mat.Row1.xyzw),
                vec4.Dot(vec.xyzw, mat.Row2.xyzw),
                vec4.Dot(vec.xyzw, mat.Row3.xyzw));
                
        }
    }
}
