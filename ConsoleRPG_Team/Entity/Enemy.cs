using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleRPG_Team.Entity
{
    internal class Enemy : Entity
    {
        private int enemyID;
        private string Reward;

        Random random = new Random();

        public Enemy()
        {
            name = "미니언";
        }
        public override void Attack(Entity target)
        {
            int randomAtk = AtkDiff();
            target.Health -= randomAtk;
            Console.WriteLine($"{target.name}의 체력을 {randomAtk} 깎았습니다.");
        }
    }
    
}
