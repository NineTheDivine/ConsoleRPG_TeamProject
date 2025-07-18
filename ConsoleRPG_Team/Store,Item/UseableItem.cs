using ConsoleRPG_Team.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleRPG_Team.Store_Item
{
    internal class UseableItem : Item
    {
        public int healAmount;
        public int manaAmount;

        public UseableItem(int ID, string name, string description, int price, ItemType type, int heal, int mana)
            :base(ID, name, 0 , description, price, type, 0)
        {
            this.healAmount = heal;
            this.manaAmount = mana;
        }

        public UseableItem()
        {
        }

        public void use(Entity target)
        {

            if(healAmount > 0)
            {
                target.health += this.healAmount;
                if (target.health > target.maxHealth)
                {
                    target.health = target.maxHealth;
                }
                Console.WriteLine($"체력이 {healAmount}만큼 회복됐습니다! 현재체력{target.health}");
            }
            if(manaAmount > 0)
            {
                target.mana += this.manaAmount;
                if(target.mana > target.maxMana)
                {
                    target.mana = target.maxMana;
                }
                Console.WriteLine($"마나가 {manaAmount}만큼 회복됐습니다! 현재마나{target.health}");
            }    
        }
    }
}
