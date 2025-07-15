using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConsoleRPG_Team.Scenes;
using ConsoleRPG_Team.Entity;

namespace ConsoleRPG_Team
{
    internal static class GameManager
    {
        static SceneType current = SceneType.None;
        static int? prevHealth = null;

        static void OnPlay()
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
                        current = new BattleScene(/* new EnemyWave() */).OnSceneEnter(); break;
                    case SceneType.BattleEndScene:
                        current = new BattleEndScene().OnSceneEnter(); break;
                    case SceneType.GameOverScene:
                        current = new GameOverScene().OnSceneEnter(); break;




                    case SceneType.Quit:
                        return;
                }
            } while (true);
        }
    }
}
