using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleRPG_Team.Scene
{
    internal class GameStartScene : Scene
    {
        public GameStartScene() 
        {
            this.inputstream = new string[]
                {
                    "1. 상태 보기",
                    "2. 전투 시작"
                };
        }
        public override void OnSceneEnter() 
        {
            AskInput(1, 2);
        }
    }
}
