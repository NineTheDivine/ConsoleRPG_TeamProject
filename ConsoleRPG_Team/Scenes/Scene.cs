using System;
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
        StatusScene
    };

    internal abstract class Scene
    {
        protected string[] inputstream = new string[0];
        public abstract SceneType OnSceneEnter();
        protected virtual int? AskInput(int min, int max)
        {
            int? input = null;
            do
            {
                Console.WriteLine();
                foreach(string str in  this.inputstream)
                    Console.WriteLine(str);
                Console.WriteLine();
                Console.WriteLine("원하시는 행동을 입력해주세요.");
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
