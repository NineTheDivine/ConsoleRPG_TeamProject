using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleRPG_Team.Scenes
{
    internal class InventoryScene : Scene
    {
        public InventoryScene()
        {
            this.inputstream = new string[]
            {
                "0. 나가기"
            };
        }

        public override SceneType OnSceneEnter()
        {
            GameManager.playerInstance.InventorySystem();

            int? input = AskInput(0, 0, this.inputstream);
            switch (input)
            {
                case 0:
                    return SceneType.StartScene;
                default:
                    Console.WriteLine("Unexpected Behaviour at StatusScene");
                    return SceneType.Quit;
            }
        }
    }
}
