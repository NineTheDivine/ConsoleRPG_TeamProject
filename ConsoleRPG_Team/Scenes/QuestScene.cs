using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConsoleRPG_Team.Quests;

namespace ConsoleRPG_Team.Scenes
{
    internal class QuestScene : Scene
    {
        private string[] acceptstream = new string[]
            {
                "1. 수락",
                "2. 거절"
            };
        public QuestScene() 
        {
            this.inputstream = new string[]
                {
                    "0. 이전으로"
                };
        }
        public override SceneType OnSceneEnter()
        {
            Console.ForegroundColor = ConsoleColor.DarkRed;
            Console.WriteLine("Quest!!");
            Console.WriteLine();

            int? input = null;
            Console.ResetColor();
            /* Need to be fixed*/
            do
            {
                int questIndex = 1;
                Quest quest = new Quest(new QuestID(QuestType.BattleWin, 0));
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.Write("{0:2}. ", questIndex.ToString());
                Console.ResetColor();
                Console.WriteLine(quest.questInfo.name);

                input = AskInput(0, 1, this.inputstream);
                if (input != null && input != 0)
                {
                    int? acceptInput = null;
                    do
                    {
                        Console.ForegroundColor = ConsoleColor.DarkRed;
                        Console.WriteLine("Quest!!");
                        Console.WriteLine();
                        Console.ResetColor();

                        Console.WriteLine(quest.questInfo.name);
                        Console.WriteLine();
                        Console.WriteLine(quest.questInfo.description);
                        acceptInput = AskInput(1,2, this.acceptstream);
                    } while (acceptInput == null);
                    if (acceptInput == 1)
                    {
                        Console.WriteLine("{0}을 수락하셨습니다." , quest.questInfo.name);
                        quest.OnQuestSelected();
                        input = 0;
                    }
                    else
                        input = null;
                }
            } while (input == null);

            return SceneType.StartScene;
        }
    }
}
