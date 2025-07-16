using ConsoleRPG_Team.Store_Item;
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
        public bool isEXP = false;

        public Item dropItem;
        public int dropRate;
        public Enemy(string name, int level, int health, int atk, int dropRate , Item dropItem)
        {
            this.name = name;
            this.level = level;
            this.health = health;
            this.atk = atk;
            this.exp = level * 3;
            this.dropRate = dropRate;
            this.dropItem = dropItem;
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
            this.dropRate = e.dropRate;
            this.dropItem = e.dropItem;
        }

        public void DropItem()
        {
            Random random = new Random();
            int dropChance = random.Next(1, 101);
            if(dropRate > dropChance)
            {
                Console.WriteLine($"{dropItem.item_Name}을 흭득했습니다.");
                GameManager.playerInstance.inventory.Add(dropItem);
            }
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
