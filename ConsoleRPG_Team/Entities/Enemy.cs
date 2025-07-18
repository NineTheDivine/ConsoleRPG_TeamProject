using ConsoleRPG_Team.Store_Item;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleRPG_Team.Entities
{
    enum EnemyType 
    {
        None = -1,
        Minion = 0,
        Voidspawn,
        CannonMinion,
    };
    internal class Enemy : Entity
    {
        public EnemyType enemyType { get; private set; }
        public bool isEXP = false;

        public Item dropItem;

        public int dropRate;
        public int dropMoney {  get; private set; }
        public Enemy(EnemyType eT, string name, int level, int health, int atk, int dropRate , Item dropItem ,int dropMoney )
        {
            this.enemyType = eT;
            this.name = name;
            this.level = level;
            this.health = health;
            this.atk = atk;
            this.exp = level * 3;
            this.dropRate = dropRate;
            this.dropItem = dropItem;
            this.dropMoney = dropMoney;
        }

        public Enemy(Enemy e)
        {
            this.enemyType = e.enemyType;
            this.name = e.name;
            this.level = e.level;
            this.health = e.health;
            this.atk = e.atk;
            this.exp = e.exp;
            this.isEXP = false;
            this.isDead = false;
            this.dropRate = e.dropRate;
            this.dropItem = e.dropItem;
            this.dropMoney = e.dropMoney;
        }

        public override int AtkDiff()
        {
            int damage = base.AtkDiff() - GameManager.playerInstance.GetDef();
            return Math.Max(0, damage);
        }
        public void DropItem()
        {
            int dropChance = random.Next(1, 101);
            if (dropRate >= dropChance)
            {
                Console.WriteLine($"{dropItem.item_Name}을 흭득했습니다.");
                GameManager.playerInstance.GetItem(dropItem);
            }
            Console.WriteLine($"{dropMoney}G을 흭득했습니다.");
            GameManager.playerInstance.gold += dropMoney;
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
