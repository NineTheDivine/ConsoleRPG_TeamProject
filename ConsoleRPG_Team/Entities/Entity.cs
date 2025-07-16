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
        public int health { get; set; }
        public int maxHealth { get; protected set; }
        public bool isDead { get; protected set; } = false;

        public int exp { get; set; }


        

        protected Random random = new Random();

        public  void Attack(Entity target)
        {
            int attackMiss = random.Next(1, 101);

            if (isDead)
                return;

            if ( attackMiss <= 10)
            {
                Console.WriteLine("{0:5} 의 공격!", this.name);
                Console.WriteLine($"{target.name}은 공격을 회피했다!");
            }
            else
            {
                int randomAtk = AtkDiff();
                target.health -= randomAtk;
                if (target.health <= 0)
                {
                    target.health = 0;
                    target.isDead = true;

                }
                Console.WriteLine("{0:5} 의 공격!", this.name);
                Console.WriteLine($"{target.name}의 체력을 {randomAtk} 깎았습니다.");
            }       
         }

        public virtual int AtkDiff()
        {
            int randomAtk = random.Next(atk - (atk / 10), atk + (atk / 10) + 1); //현재 int라서 ATK가 10 이하면 랜덤이 안됨

            float caculAtk = atk / 10f;

            if (caculAtk >= 0.5f && caculAtk < 1f)
                return random.Next(atk - 1, atk + 2);

            else
                return randomAtk;
        }

    }
}
