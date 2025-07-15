using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleRPG_Team.Scenes
{
    internal class BattleScene : Scene
    {
        public BattleScene() { }
        public override SceneType OnSceneEnter()
        {
            //Need to be overrided

            return SceneType.None;
        }

        protected override int? AskInput(int min, int max)
        {
            //need to be overrided

            return null;
        }
    }
}
