using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleRPG_Team.Scenes
{
    internal class StatusScene : Scene
    {
        public StatusScene() 
        {
            this.inputstream = new string[]
                {
                    "0. 나가기"
                };
        }
        public override SceneType OnSceneEnter()
        {
            //Need to implement

            //Need to implement End
            int? input = AskInput(0, 0);
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
