using GeometryDashAPI.Attributes;
using GeometryDashAPI.Levels.GameObjects.Default;
using GeometryDashAPI.Levels.GameObjects.Triggers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DOTGD.Libs.DotPieTriggers
{
    internal class Gradient:Trigger
    {
        [GameProperty("174",(int)0)]
        public int blending { get;set; }
        [GameProperty("21", (short)1004, false)]
        public short ColorBase { get; set; } = 1004;
        [GameProperty("22", (short)1004, false)]
        public short Color2 { get; set; } = 1004;

        [GameProperty("202",1)]
        public int layer { get;set; }
        [GameProperty("203",0)]
        public int bl { get; set; }
        [GameProperty("204", 0)]
        public int br { get; set; }
        [GameProperty("205", 0)]
        public int tl { get; set; }
        [GameProperty("206", 0)]
        public int tr { get; set; }
        [GameProperty("207",(bool)true)]
        public bool vertexMode { get; set; }
        [GameProperty("209",0)]
        public int id { get; set; }
        public Gradient():base(2903) { }
        
    }
}
