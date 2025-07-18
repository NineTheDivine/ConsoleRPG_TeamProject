using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleRPG_Team.Entities
{
    internal class Rogue : Player
    {
        public Rogue()
        {
            throw new NotImplementedException();
        }
        public Rogue(Player p) : base(p)
        {
            playerClass = PlayerClass.Rogue;
            chooseClass = true;
            criticalPro += 10;
            Console.WriteLine("치명타확률 10% 증가");
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

            int damage = RogueSkill();
            Console.WriteLine($"[백스탭] {name}이 적의 뒤를 찔렀다!");
            Console.WriteLine();
            Console.WriteLine("{0:5} 의 연속 찌르기!", this.name);
            int hitCount = random.Next( 2, 5 );
            int actualHit = 0;
            while(actualHit < hitCount)
            {
                Attack(enemy, RogueSkill(), true);
                if (enemy.isDead)
                    break;
                actualHit++;
            }
            Console.WriteLine();
            Console.WriteLine($"총 {actualHit} 번 공격했다!");
            return true;
        }
        private int RogueSkill()
        {
            int dmg = (int)(AtkDiff() * 0.7f);
            return dmg;
        }
    }
}
