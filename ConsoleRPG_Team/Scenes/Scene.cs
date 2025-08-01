﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleRPG_Team.Scenes
{
    enum SceneType 
    {
        Quit = -1,
        None = 0,
        StartScene = 1,
        BattleScene,
        BattleEndScene,
        GameOverScene,
        StatusScene,
        StoreScene, // 임시
        InventoryScene, // 임시
        QuestScene
    };

    internal abstract class Scene
    {
        protected string[] inputstream = new string[0];
        public abstract SceneType OnSceneEnter();
        protected virtual int? AskInput(int min, int max, string[] stringinputs, string ask = "원하시는 행동을 입력해주세요.")
        {
            int? input = null;
            do
            {
                Console.WriteLine();
                foreach(string str in stringinputs)
                    Console.WriteLine(str);
                Console.WriteLine();
                Console.WriteLine(ask);
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.Write(">> ");
                string s = Console.ReadLine();
                Console.ResetColor();
                s = s.Trim(' ');
                if (int.TryParse(s, out int temp) && temp >= min && temp <= max)
                {
                    return temp;
                }
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine();
                Console.WriteLine("잘못된 입력입니다.");
                Console.ResetColor();
            } while (input == null);
            return null;
        }
    }
}
