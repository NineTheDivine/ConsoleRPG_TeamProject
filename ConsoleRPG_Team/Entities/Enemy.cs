using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleRPG_Team.Entities
{
    internal class Enemy : Entity
    {
        private int enemyID;
        private string Reward;

        public Enemy(string name, int level, int health)
        {
            this.name = name;
            this.level = level;
            this.Health = health;
        }

        public override void Attack(Entity target)
        {
            int randomAtk = AtkDiff();
            target.Health -= randomAtk;
            Console.WriteLine($"{target.name}의 체력을 {randomAtk} 깎았습니다.");
        }
    }
    
}
