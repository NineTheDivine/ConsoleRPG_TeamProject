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

        public virtual void Attack(Entity target)
        {
            int attackMiss = random.Next(1, 101);

            if (isDead)
                return;
            if (attackMiss <= 10)
            {
                Console.WriteLine("{0:5} 의 공격!", this.name);
                Console.WriteLine($"{target.name}은 공격을 회피했다!");
            }
            else
            {
                int randomAtk = AtkDiff();
                if(randomAtk == 0)
                {
                    Console.WriteLine("{0:5} 의 공격!", this.name);
                    Console.WriteLine($"{target.name}은 흠도 나지 않았다!");
                }
                else
                {
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
         }

        public virtual int AtkDiff()
        {
            int fluctuation = Math.Max(1, (int)Math.Round(atk * 0.1f));
            int minAtk = Math.Max(0, atk - fluctuation); 
            int maxAtk = atk + fluctuation + 1;
            int randomAtk = random.Next(minAtk, maxAtk);

            return randomAtk;
        }
    }
}
