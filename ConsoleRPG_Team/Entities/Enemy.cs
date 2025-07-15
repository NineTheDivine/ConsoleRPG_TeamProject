using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleRPG_Team.Entities
{
    internal class Enemy : Entity
    {
        //protected int enemyID;
        //protected string Reward;

        public Enemy(string name, int level, int health, int atk)
        {
            this.name = name;
            this.level = level;
            this.health = health;
            this.atk = atk;
        }

        public Enemy(Enemy e)
        {
            this.name = e.name;
            this.level = e.level;
            this.health = e.health;
            this.atk = e.atk;
        }
    }
    
}
