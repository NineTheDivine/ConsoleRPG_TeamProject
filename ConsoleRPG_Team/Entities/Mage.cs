using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleRPG_Team.Entities
{
    internal class Mage : Player
    {
        public Mage()
        {
            throw new NotImplementedException();
        }
        public Mage(Player p) : base(p)
        {
            playerClass = PlayerClass.Mage;
            chooseClass = true;
            atk += 1;
            maxMana += 20;
            mana += 20;
            Console.WriteLine("공격력 1증가 최대마나 +20");
        }
        public override bool UseSkill(List<Enemy> targets)
        {
            if (targets.Count == 0)
            {
                Console.WriteLine("잘못된 대상 지정입니다.");
                return false;
            }
            if (this.mana < 20)
            {
                Console.WriteLine($"{name}은 마나가 부족해서 스킬을 사용할수 없다.");
                return false;
            }

            mana -= 20;

            int damage = MageSkill();
            Console.WriteLine($"[파이어볼] {name}이 화염구를 발사했다!");
            Console.WriteLine();
            Console.WriteLine("{0:5} 의 파이어볼!!", this.name);
            foreach (Enemy enemy in targets)
            {
                if (enemy.isDead)
                    continue;
                Attack(enemy, MageSkill(), true);
            }
            Console.WriteLine();
            return true;
        }
        private int MageSkill()
        {
            int dmg = (int)(AtkDiff() * 2.0f);
            return dmg;
        }
    }
}
