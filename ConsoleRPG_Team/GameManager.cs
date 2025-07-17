using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConsoleRPG_Team.Scenes;
using ConsoleRPG_Team.Entities;

namespace ConsoleRPG_Team
{
    internal static class GameManager
    {
        static SceneType current = SceneType.None;
        public static Player playerInstance = new Player();
        public static int? waveEnemyCount = null;
        public static void OnPlay()
        {
            do
            {
                switch (current)
                {
                    case SceneType.None:
                        current = SceneType.StartScene; break;
                    case SceneType.StartScene:
                        current = new GameStartScene().OnSceneEnter(); break;
                    case SceneType.StatusScene:
                        current = new StatusScene().OnSceneEnter(); break;
                    case SceneType.BattleScene:
                        current = new BattleScene(new EnemyWave(1, 3, 2, 3)).OnSceneEnter(); break;
                    case SceneType.BattleEndScene:
                        current = new BattleEndScene().OnSceneEnter(); break;
                    case SceneType.GameOverScene:
                        current = new GameOverScene().OnSceneEnter(); break;
                    //임시
                    case SceneType.StoreScene:
                        current = new StoreScene().OnSceneEnter(); break;
                    //임시
                    case SceneType.InventoryScene:
                        current = new InventoryScene().OnSceneEnter(); break;
                    case SceneType.QuestScene:
                        current = new QuestScene().OnSceneEnter(); break;
                        




                    case SceneType.Quit:
                        return;
                }
            } while (true);
        }
    }
}
