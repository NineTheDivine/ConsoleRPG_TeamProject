using ConsoleRPG_Team.Quests;
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
        public int mana { get; set; } // 마나
        public int maxMana { get; protected set; }
        public bool isDead { get; set; } = false;

        public int exp { get; set; }

        protected Random random = new Random();

        public virtual void Attack(Entity target, int? damage = null, bool isSkill = false)
        {
            if (isDead)
                return;
            if (damage == null)
                damage = AtkDiff();
            if (!isSkill)
                Console.WriteLine("{0:5} 의 공격!", this.name);

            int attackMiss = random.Next(1, 101);
            if (attackMiss <= 10)
                Console.WriteLine($"{target.name}은 공격을 회피했다!");
            else
                target.TakeDamage((int)damage);

         }
        public virtual void TakeDamage(int damage)
        {
            if (damage == 0)
            {
                Console.WriteLine($"{this.name}은 흠도 나지 않았다!");
                return;
            }
            this.health -= damage;
            if (this.health <= 0)
            {
                this.health = 0;
                this.isDead = true;
            }
            Console.WriteLine($"{this.name}의 체력을 {damage} 깎았습니다.");
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
