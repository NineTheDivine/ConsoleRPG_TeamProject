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
                "0. 이전으로"
            };
        private string[] rewardstream = new string[]
            {
                "1. 보상 수령",
                "0. 이전으로"
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
                    Console.WriteLine(QuestPool[i].questInfo.name + (GameManager.playerInstance.currentQuest == QuestPool[i] ? " [진행중인 퀘스트] " : ""));
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
                        if (quest == GameManager.playerInstance.currentQuest)
                        {
                            if (quest.isClear)
                            {
                                int? confirminput = null;
                                confirminput = AskInput(0, 1, this.rewardstream);
                                if (confirminput == 1)
                                {
                                    GameManager.playerInstance.currentQuest.RecieveReward();
                                    QuestPool[(int)input - 1] = null;
                                    _InitQuest();
                                    acceptInput = 0;
                                }
                                acceptInput = 0;
                            }
                            else
                                acceptInput = AskInput(0, 0, this.inputstream);
                        }
                        else
                        {
                            acceptInput = AskInput(0, 1, this.acceptstream);
                            if (acceptInput == 1 && GameManager.playerInstance.currentQuest != null)
                            {
                                int? confirmInput = null;
                                do
                                {
                                    Console.WriteLine();
                                    Console.WriteLine("현재 수락중인 퀘스트가 있습니다. - {0}", GameManager.playerInstance.currentQuest.questInfo.name);
                                    Console.WriteLine("해당 퀘스트를 수락하면, 이전 퀘스트의 진척도가 초기화 됩니다. 그래도 진행하시겠습니까?");
                                    confirmInput = AskInput(0, 1, acceptstream);
                                } while (confirmInput == null);
                                if (confirmInput == 1)
                                    acceptInput = 1;
                                else
                                    acceptInput = null;
                            }
                        }
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
