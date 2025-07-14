using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleRPG_Team.Scene
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
        public override void OnSceneEnter()
        {
            AskInput(0, 0);
        }
        
    }
}
