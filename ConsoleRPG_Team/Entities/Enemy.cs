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
        public bool isEXP = false;

        public Enemy(string name, int level, int health, int atk)
        {
            this.name = name;
            this.level = level;
            this.health = health;
            this.atk = atk;
            this.exp = level * 3;
        }

        public Enemy(Enemy e)
        {
            this.name = e.name;
            this.level = e.level;
            this.health = e.health;
            this.atk = e.atk;
            this.exp = e.exp;
            this.isEXP = false;
            this.isDead = false;
        }

        public void GiveExp()
        {
            if(this.isDead && !this.isEXP)
            {
                GameManager.playerInstance.exp += exp;
                isEXP = true;
            }
        }
    }
}
