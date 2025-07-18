using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleRPG_Team.Scenes
{
    internal class GameOverScene : Scene
    {
        public GameOverScene()
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

            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("You Lose");
            Console.WriteLine();

            Console.ResetColor();
            GameManager.waveEnemyCount = null;

            Console.WriteLine();
            Console.WriteLine("Lv.{0:D2} {1:5} ( {2:8} )", GameManager.playerInstance.level, GameManager.playerInstance.name, GameManager.playerInstance.playerClass.ToString());
            Console.WriteLine("HP : {0:-3} -> {1:-3}", GameManager.playerInstance.beforeHealth.ToString(), GameManager.playerInstance.health.ToString());
            GameManager.playerInstance.beforeHealth = null;
            int? input = AskInput(0, 0, this.inputstream);
            switch (input)
            {
                case 0:
                    return SceneType.Quit;
                default:
                    Console.WriteLine("Unexpected Behaviour at GameOverScene");
                    return SceneType.Quit;
            }
        }
        
    }
}
