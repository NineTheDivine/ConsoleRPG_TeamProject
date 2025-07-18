using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleRPG_Team.Entities
{
    internal class Warrior : Player
    {
        public Warrior()
        {
            throw new NotImplementedException();
        }
        public Warrior(Player p) : base(p)
        {
            playerClass = PlayerClass.Warrior;
            chooseClass = true;
            maxHealth += 10;
            health += 10;
            if(health > maxHealth)
                health = maxHealth;
            atk += 2;
            Console.WriteLine("최대체력 10 증가 공격력 2증가");
        }
        public override bool UseSkill(List<Enemy> targets)
        {
            if (targets.Count != 1)
            {
                Console.WriteLine("잘못된 대상 지정입니다.");
                return false;
            }
            Enemy enemy = targets[0];
            if (this.mana < 20)
            {
                Console.WriteLine($"{name}은 마나가 부족해서 스킬을 사용할수 없다.");
                return false;
            }

            mana -= 20;

            int damage = WarriorSkill();
            Console.WriteLine($"[더블 스트라이크] {name}이 강력한 일격을 날렸다!");
            Console.WriteLine();
            Console.WriteLine("{0:5} 의 파워 스트라이크!!", this.name);
            Attack(enemy, WarriorSkill(), true);
            Console.WriteLine();
            return true;
        }
        private int WarriorSkill()
        {
            int dmg = (int)(AtkDiff() * 2.0f);
            return dmg;
        }
    }
}

