using ConsoleRPG_Team.Store_Item;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleRPG_Team.Scenes
{
    internal class StoreScene : Scene //임시
    {
        Store store = new Store();
        public StoreScene()
        {
            this.inputstream = new string[]
            {
                "0. 나가기",
                "1. 구매하기",
                "2. 판매하기",
                "3. 업그레이드"
            };
        }
        public override SceneType OnSceneEnter()
        {
            int? input = AskInput(0, 3, this.inputstream);
            switch(input)
            {
                case 0:
                    return SceneType.StartScene;

                case 1:
                    store.Buy();
                    return SceneType.StoreScene;

                case 2:
                    store.Sell();
                    return SceneType.StoreScene;

                case 3:
                    store.Upgrade();
                    return SceneType.StoreScene;

                default:
                    Console.WriteLine("Unexpected Behaviour at StatusScene");
                    return SceneType.Quit;
            }

        }
    }
}
