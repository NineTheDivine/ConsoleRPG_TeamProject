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
            Console.ForegroundColor = ConsoleColor.DarkRed;
            Console.WriteLine("Battle!! - Result");
            Console.WriteLine();

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Victory");
            Console.WriteLine();

            Console.ResetColor();
            Console.WriteLine("던전에서 몬스터 {0}마리를 잡았습니다.", GameManager.waveEnemyCount);
            GameManager.waveEnemyCount = null;

            Console.WriteLine();
            Console.WriteLine("Lv.{0:D2} Chad ( {1:4} )", GameManager.playerInstance.level, /*Need to be fixed */GameManager.playerInstance.name);
            Console.WriteLine("HP : {0:-3} -> {1:-3}", GameManager.playerInstance.beforeHealth.ToString(), GameManager.playerInstance.health.ToString());
            Console.WriteLine($"{GameManager.playerInstance.getExp} 경험치를 흭득했습니다!");
            GameManager.playerInstance.getExp = 0;
            GameManager.playerInstance.LevelUp();
            GameManager.playerInstance.beforeHealth = null;


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
