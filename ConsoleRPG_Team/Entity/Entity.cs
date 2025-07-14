using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleRPG_Team.Entity
{
    internal abstract class Entity
    {
        public string name;
        protected int level;
        private int atk = 10;
        private int health;
        protected int maxHealth;
        protected bool isDead = false;

        //public abstract void OnTurn();

        public abstract void Attack(Player player);
        public abstract void Attack(Enemy enemy);

        public abstract int AtkDiff();

        //public abstract void OnDamageDelt();

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
