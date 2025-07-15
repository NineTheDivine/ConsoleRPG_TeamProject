using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleRPG_Team.Scenes
{
    internal class GameStartScene : Scene
    {
        public GameStartScene() 
        {
            this.inputstream = new string[]
                {
                    "1. 상태 보기",
                    "2. 전투 시작",
                    "0. 게임 종료"
                };
        }
        public override SceneType OnSceneEnter() 
        {
            Console.WriteLine("스파르타 던전에 오신 여러분 환영합니다.");
            Console.WriteLine("이제 전투를 시작할 수 있습니다.");
            int? input = AskInput(0, 2, inputstream);
            switch (input)
            {
                case 0:
                    return SceneType.Quit;
                case 1:
                    return SceneType.StatusScene;
                case 2:
                    return SceneType.BattleScene;
                default:
                    Console.WriteLine("Unexpected Behaviour at GameStartScene");
                    return SceneType.Quit;
            }

        }
    }
}
