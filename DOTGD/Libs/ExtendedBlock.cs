using System;
using GeometryDashAPI;
using GeometryDashAPI.Attributes;
using GeometryDashAPI.Levels.GameObjects.Default;
namespace DotPieGDLib
{
    internal class ExtendedBlock:BaseBlock
    {
        [GameProperty("128", (float)1,false)]
        public float scaleX { get; set; } = 1;
        [GameProperty("129", (float)1,false)]
        public float scaleY { get; set; } = 1;
        public ExtendedBlock() { }
        public ExtendedBlock(int id) : base(id) { }
    }
}
