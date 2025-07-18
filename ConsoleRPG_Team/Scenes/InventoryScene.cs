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
            Console.WriteLine("\n플레이어 인벤토리입니다.");

            GameManager.playerInstance.InventorySystem();

            return SceneType.StartScene;

            //int? input = AskInput(0, 0, this.inputstream);
            //switch (input)
            //{
            //    case 0:
            //        
            //    default:
            //        Console.WriteLine("Unexpected Behaviour at StatusScene");
            //        return SceneType.Quit;
            //}
        }
    }
}
