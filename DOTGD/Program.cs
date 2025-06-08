
using DOTGD.Libs;
using DOTGD.Libs._3DExtensions;
using DOTGD.Libs._3DExtensions.Primitives;
using DOTGD.Libs._3DExtensions.Rendering;
using DotPieGDLib;
using GeometryDashAPI.Data;
using GeometryDashAPI.Levels;
using GeometryDashAPI.Levels.Structures;
using GlmSharp;


public class Program
{
    public static async Task Main()
    {
        
        
        LocalLevelEditing levelEditing =await LocalLevelEditing.buildInstance("DotPieC");
        Level editedLevel=levelEditing.getEditedLevel();
        LoadModel modelLoader = new LoadModel(@"C:\Users\admin\Documents\untitled.obj");
        mat4 model = mat4.Identity;
        model = mat4.Translate(0,0,0);
        mat4 projectMatrix = mat4.Frustum(-1,1,-1,1,.2f,100);
        

       
        
        ToGDRenderEngine.Render(RgbColor.FromHex("#ff0000"),-vec3.UnitZ,editedLevel,modelLoader, model, projectMatrix,1920,1080);
        List<ExtendedBlock> blocks = new List<ExtendedBlock>();
        blocks.AddRange([new ExtendedBlock(1), new ExtendedBlock(1), new ExtendedBlock(1), new ExtendedBlock(1)]);
        levelEditing.saveLevel(editedLevel);
    }
    
    
}
