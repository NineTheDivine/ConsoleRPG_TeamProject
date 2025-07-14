using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleRPG_Team.Scene
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
        public override void OnSceneEnter()
        {
            AskInput(0, 0);
        }
    }
}
