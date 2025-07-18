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
            //Write Player info
            Console.ForegroundColor = ConsoleColor.DarkMagenta;
            Console.WriteLine("상태 보기");
            Console.ResetColor();
            Console.WriteLine("캐릭터의 정보가 표시됩니다.");
            Console.WriteLine();

            //Console.WriteLine("Lv. {0:D2}", GameManager.playerInstance.level);
            Console.WriteLine($"LV {GameManager.playerInstance.level}  EXP: {GameManager.playerInstance.exp} / {GameManager.playerInstance.maxExp}");
            Console.WriteLine("{0:5} ( {0:4} )", /*need to be fixed*/ GameManager.playerInstance.name);
            Console.WriteLine($"직업 :{GameManager.playerInstance.playerClass}");
            Console.WriteLine("공격력 : {0:-3}", GameManager.playerInstance.GetATK().ToString());
            Console.WriteLine("방어력 : {0:-3}", GameManager.playerInstance.GetDef().ToString());
            Console.WriteLine("체 력 : {0:-3} / {1:-3}", GameManager.playerInstance.health.ToString(), GameManager.playerInstance.maxHealth.ToString());
            Console.WriteLine("마 나 : {0:-3} / {1:-3}", GameManager.playerInstance.mana.ToString(), GameManager.playerInstance.maxMana.ToString());
            Console.WriteLine("Gold : {0:-6} G", GameManager.playerInstance.gold.ToString());
            //End Player info

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
