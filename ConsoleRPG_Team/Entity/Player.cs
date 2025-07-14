using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleRPG_Team.Entity
{
    internal class Player : Entity
    {
        private int def;
        private int gold;

        Random random = new Random();

        public Player()
        {
            name = "주인공";
        }
        //public override void OnTurn()
        //{
        //    Attack();
        //}

        public override void Attack(Enemy enemy)
        {
            int randomAtk = AtkDiff();
            enemy.Health -= randomAtk;
            Console.WriteLine($"{enemy.name}의 체력을 {randomAtk} 깎았습니다.");
        }

        public override void Attack(Player player)
        {
        }

        public override int AtkDiff() // 데미지 +- 10% 계산
        {
            int randomAtk = random.Next(Atk - (Atk / 10), Atk + (Atk / 10) + 1);

            return randomAtk;
        }

        //public override void OnDamageDelt()
        //{

        //}
    }
}
