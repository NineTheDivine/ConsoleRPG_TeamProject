using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleRPG_Team.Entity
{
    internal class Enemy : Entity
    {
        private int enemyID;
        private string Reward;

        Random random = new Random();

        public Enemy()
        {
            name = "미니언";
        }
        //public override void OnTurn()
        //{
        //    Attack();
        //}

        public override void Attack(Player player)
        {
            int randomAtk = AtkDiff();
            player.Health -= randomAtk;
            Console.WriteLine($"{player.name}의 체력을 {randomAtk} 깎았습니다.");
        }

        public override void Attack(Enemy enemy)
        {
        }

        public override int AtkDiff() // 데미지 +- 10% 계산
        {
            int randomAtk = random.Next(Atk - (Atk / 10), Atk + (Atk / 10) + 1);

            return randomAtk;
        }

        //public override void OnDamageDelt()
        //{


        //    Console.WriteLine($"체력이 {AtkDiff()} 만큼 깎였습니다.");


        //}
    }
    
}
