using ConsoleRPG_Team.Store_Item;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleRPG_Team.Entities
{
    internal class EnemyWave
    {
        int minLevel;
        int maxLevel;
        int minCount;
        int maxCount;

        Random random = new Random();

        public List<Enemy> spawnEnemies = new List<Enemy>();
        
        public EnemyWave(int minLevel, int maxLevel, int minCount, int maxCount)
        {
            this.minLevel = minLevel;
            this.maxLevel = maxLevel;
            this.minCount = minCount;
            this.maxCount = maxCount;
            SpawnEnemyWave();
        }

        public void SpawnEnemyWave()
        {
            int spawnCount = random.Next(minCount , maxCount + 1);

            List<Enemy> enmies = new List<Enemy>();
            {
                enmies.Add(new Enemy(EnemyType.Minion,"미니언", 1, 10, 1, 20, Item.items[0] , 100 ));
                enmies.Add(new Enemy(EnemyType.Voidspawn,"공허충", 2, 15, 3 , 20 , Item.items[1] , 150));
                enmies.Add(new Enemy(EnemyType.CannonMinion,"대포미니언", 3, 20, 5 , 5, Item.items[2] , 200));
            };

            enmies = enmies.Where(enemy => enemy.level >= minLevel && enemy.level <= maxLevel).ToList();

            for(int i = 0; i < spawnCount; i++)
            {
                if(enmies.Count == 0)
                {
                    break;
                }

                Enemy selected = new Enemy(enmies[random.Next(enmies.Count)]);
                spawnEnemies.Add(selected);   
            }
        }
    }
}
