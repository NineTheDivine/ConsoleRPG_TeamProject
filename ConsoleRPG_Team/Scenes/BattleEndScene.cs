using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleRPG_Team.Scenes
{
    internal class BattleEndScene : Scene
    {
        public BattleEndScene() 
        {
            this.inputstream = new string[]
                {
                    "0. 다음"
                };
        }
        public override SceneType OnSceneEnter()
        {
            int? input = AskInput(0, 0, this.inputstream);
            switch (input)
            {
                case 0:
                    return SceneType.StartScene;
                default:
                    Console.WriteLine("Unexpected Behaviour at BattleScene");
                    return SceneType.Quit;
            }
        }
    }
}
