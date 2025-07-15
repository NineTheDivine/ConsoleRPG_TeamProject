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

        public Enemy(string name, int level, int health)
        {
            this.name = name;
            this.level = level;
            this.health = health;
        }
    }
    
}
