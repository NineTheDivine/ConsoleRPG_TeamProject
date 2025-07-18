using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using ConsoleRPG_Team.Entities;

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
            QuestEventBus.Publish(new Quests.QuestID(Quests.QuestType.BattleWin, 0));
            GameManager.playerInstance.getExp = 0;
            GameManager.playerInstance.LevelUp();

            if (GameManager.playerInstance.playerClass == PlayerClass.None && GameManager.playerInstance.level >= 3)
            {

                int? selected = null;
                do
                {
                    Console.WriteLine();

                    selected = AskInput(1, 3, new string[] { "전직할수 있습니다 어떤 직업으로 하시겠습니까?.", "1.전사 2.마법사 3.도적" });
                } while (selected == null);
                switch (selected)
                {
                    case 1:
                        Console.WriteLine("전사를 선택하셨습니다.");
                        GameManager.playerInstance = new Warrior(GameManager.playerInstance);
                        break;
                    case 2:
                        Console.WriteLine("마법사를 선택하셨습니다.");
                        GameManager.playerInstance = new Mage (GameManager.playerInstance);
                        break;
                    case 3:
                        Console.WriteLine("도적을 선택하셨습니다.");
                        GameManager.playerInstance = new Rogue(GameManager.playerInstance);
                        break;
                    default:
                        Console.WriteLine("당신은 아무런 직업도 선택하지 못했습니다 - Error at BattleEndScene");
                        break;
                }
            }

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
