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
        static Quest?[] QuestPool = new Quest?[3] { null, null, null};
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
            _InitQuest();
            
        }

        public static void _InitQuest()
        {
            for (int i = 0; i < QuestPool.Length; i++)
            {
                if (QuestPool[i] == null)
                    QuestPool[i] = new Quest(new Random().Next(0,QuestInfo.QUESTCATEGORY.Count-1));
            }
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
                for (int i = 0; i < QuestPool.Length; i++)
                {
                    Console.ForegroundColor = ConsoleColor.Blue;
                    Console.Write("{0:2}. ", (i+1).ToString());
                    Console.ResetColor();
                    if (QuestPool[i] == null)
                        _InitQuest();
                    Console.WriteLine(QuestPool[i].questInfo.name);
                }
                input = AskInput(0, QuestPool.Length, this.inputstream);
                if (input != null && input != 0)
                {
                    int? acceptInput = null;
                    Quest quest = QuestPool[(int)input - 1];
                    do
                    {
                        Console.ForegroundColor = ConsoleColor.DarkRed;
                        Console.WriteLine("Quest!!");
                        Console.WriteLine();
                        Console.ResetColor();

                        Console.WriteLine(quest.questInfo.name);
                        Console.WriteLine();
                        Console.WriteLine(quest.questInfo.description);
                        Console.WriteLine("퀘스트 진척도 : [ {0:-3} / {1:-3} ]", quest.currentProgress.ToString(), quest.questInfo.finalProgress.ToString());
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
