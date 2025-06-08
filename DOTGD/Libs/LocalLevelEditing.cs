using GeometryDashAPI.Data;
using GeometryDashAPI.Data.Models;
using GeometryDashAPI.Levels;
using GeometryDashAPI.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DOTGD.Libs
{
    internal class LocalLevelEditing
    {

        private static LocalLevels localInstance;
        public static LocalLevels gettingLocalLevel => localInstance;
        private LevelCreatorModel levelCreatorModel;
        private LocalLevelEditing(string levelName,LocalLevels localInstance, int revision=0)
        {
            levelCreatorModel= localInstance.GetLevel(levelName,revision);
        }
        public static async Task<LocalLevelEditing> buildInstance(string levelName, int revision=0) {
            localInstance = await LocalLevels.LoadFileAsync();
           
            return new LocalLevelEditing(levelName,localInstance,revision);
        }
        public Level getEditedLevel()
        {
           return levelCreatorModel.LoadLevel();
        }
        public async void saveLevel(Level level) {
            
            if (GameProcess.GameCount() > 0)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("The game is running! Can't override the data");
                Console.ReadLine();
                return;
            }
            levelCreatorModel.SaveLevel(level);
            await localInstance.SaveAsync();
        }
    }
}
