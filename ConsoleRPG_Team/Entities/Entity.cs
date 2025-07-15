using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleRPG_Team.Entities
{
    internal abstract class Entity
    {
        public string name { get; set; }
        public int level { get; set; }
        public int atk { get; protected set; }
        public int health { get; protected set; }
        public int maxHealth { get; protected set; }
        public bool isDead = false;


        

        Random random = new Random();

        public  void Attack(Entity target)
        {
            if (isDead)
                return;
            int randomAtk = AtkDiff();
            target.health -= randomAtk;
            Console.WriteLine($"{target.name}의 체력을 {randomAtk} 깎았습니다.");
        }

        public int AtkDiff()
        {
            int randomAtk = random.Next(atk - (atk / 10), atk + (atk / 10) + 1);

            return randomAtk;
        }

    }
}
