using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleRPG_Team.Entities
{
    internal abstract class Entity
    {
        public string name;
        public int level;
        private int atk = 10;
        private int health;
        protected int maxHealth;
        protected bool isDead = false;

        Random random = new Random();

        public abstract void Attack(Entity target);

        public int AtkDiff()
        {
            int randomAtk = random.Next(Atk - (Atk / 10), Atk + (Atk / 10) + 1);

            return randomAtk;
        }
        

        public int Atk
        {
            get { return atk; }
            set { atk = value; }
        }

        public int Health
        {
            get { return health; }
            set
            {
                health = value;
                if (value <= 0)
                {
                    isDead = true;
                }
            }
        }
    }
}
